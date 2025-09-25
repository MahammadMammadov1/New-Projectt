using deneme_2.DTOs.AuthorDtos;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _authorServices;

        public AuthorController(IAuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var data = await _authorServices.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var data = await _authorServices.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto dto)
        {
            await _authorServices.CreateAsync(dto);
            return Created("", new { message = "Author created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto dto)
        {
            await _authorServices.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorServices.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteAuthor(int id)
        {
            await _authorServices.SoftDelete(id);
            return NoContent();
        }
    }
}
