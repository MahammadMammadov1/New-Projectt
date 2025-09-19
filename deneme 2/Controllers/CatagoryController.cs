using deneme_2.Database;
using deneme_2.DTOs.CatagoryDtos;
using deneme_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly AppDbContext _appDb;

        public CatagoryController(AppDbContext appDb)
        {
            this._appDb = appDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatagories()
        {
            List<Catagory> catagories =await _appDb.Catagories.ToListAsync();
            return Ok(catagories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatagory(int id)
        {
            var catagory =await _appDb.Catagories.FindAsync(id);

            if (catagory is null) return NotFound();

            CatagoryGetDto catagoryGetDto = new CatagoryGetDto()
            {
                Id = catagory.Id,
                Name = catagory.Name,
                Description = catagory.Description,
                CreatedDate = catagory.CreatedAt,
                UpdatedDate = catagory.UpdatedAt,

            };
            
            return Ok(catagory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatagory(CatagoryCreateDto dto)
        {
            Catagory catagory = new Catagory
            {
                Name = dto.Name,
                Description = dto.Description,

                

            };

            catagory.CreatedAt = DateTime.UtcNow.AddHours(4);
            catagory.UpdatedAt = DateTime.UtcNow.AddHours(4);


            await _appDb.Catagories.AddAsync(catagory);
            await _appDb.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]   
        public async Task<IActionResult> UpdateCatagory(int id, CatagoryUpdateDto dto)
        {
            var catagory =await _appDb.Catagories.FindAsync(id);
            if (catagory is null) return NotFound();

            catagory.Name = dto.Name;
            catagory.Description = dto.Description;
            catagory.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDb.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatagory(int id)
        {
            var catagory =await _appDb.Catagories.FindAsync(id);
            if (catagory is null) return NotFound();
            _appDb.Catagories.Remove(catagory);
            await _appDb.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteCatagory(int id)
        {
            var catagory =await _appDb.Catagories.FindAsync(id);
            if (catagory is null) return NotFound();
            catagory.IsDeleted = true;
            await _appDb.SaveChangesAsync();
            return NoContent();
        }

    }
}
