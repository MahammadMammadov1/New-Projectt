using deneme_2.DTOs.CatagoryDtos;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private readonly ICatagoryIServices _catagoryIServices;

        public CatagoryController(ICatagoryIServices catagoryIServices)
        {
            _catagoryIServices = catagoryIServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatagories()
        {
            var data = await _catagoryIServices.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatagory(int id)
        {
            var data = await _catagoryIServices.GetAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatagory(CatagoryCreateDto dto)
        {
            await _catagoryIServices.CreateAsync(dto);
            return Created("", new { message = "Category created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatagory(int id, CatagoryUpdateDto dto)
        {
            await _catagoryIServices.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatagory(int id)
        {
            await _catagoryIServices.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteCatagory(int id)
        {
            await _catagoryIServices.SoftDelete(id);
            return NoContent();
        }
    }
}
