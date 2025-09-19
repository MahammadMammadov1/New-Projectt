using deneme_2.DTOs.CatagoryDtos;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
                var data = await _catagoryIServices.GetAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatagory(CatagoryCreateDto dto)
        {
            try
            {
                await _catagoryIServices.CreateAsync(dto);
                return Ok(new { message = "Category created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatagory(int id, CatagoryUpdateDto dto)
        {
            try
            {
                await _catagoryIServices.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatagory(int id)
        {
            try
            {
                await _catagoryIServices.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteCatagory(int id)
        {
            try
            {
                await _catagoryIServices.SoftDelete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
