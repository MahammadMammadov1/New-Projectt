using deneme_2.Database;
using deneme_2.DTOs.AuthorDtos;
using deneme_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public AuthorController(AppDbContext appDb)
        {
            this._appDb = appDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            List<Models.Author> authors =await _appDb.Authors.Where(e => e.IsDeleted == false).ToListAsync();
            return Ok(authors);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _appDb.Authors.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (author is null) return NotFound();

            AuthorGetDto dto = new AuthorGetDto()
            {
                Id = author.Id,
                Name = author.FirstName + " " + author.LastName,
                IsDeleted = author.IsDeleted,
                CreatedDate = author.CreatedAt,
                UpdatedDate = author.UpdatedAt,
            };
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author)
        {
             Author author1 = new Author
             {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate,
                CreatedAt = DateTime.UtcNow.AddHours(4),
             };
            await _appDb.Authors.AddAsync(author1);
            await _appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto dto)
        {
            var author = _appDb.Authors.Find(id);
            if (author is null) return NotFound();
            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            author.BirthDate = dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeleteAuthor(int id)
        {
            var author = await _appDb.Authors.FirstOrDefaultAsync(e => e.Id == id);
            if (author is null) return NotFound();
             _appDb.Authors.Remove(author);
            await _appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteAuthor(int id)
        {
            var author =await _appDb.Authors.FindAsync(id);
            if (author is null) return NotFound();
            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
           await _appDb.SaveChangesAsync ();
            return Ok();
        }
    }
}
