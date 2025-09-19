using deneme_2.Database;
using deneme_2.DTOs.BookDtos;
using deneme_2.Models;
using deneme_2.Services.Implementations;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly IBookServices _bookServices;

        public BookController(AppDbContext appDb, IBookServices bookServices)
        {
            this._appDb = appDb;
            this._bookServices = bookServices;
        }

         

        [HttpGet("")]
        public async  Task<IActionResult> GetAll([FromQuery] int? authorId = null, [FromQuery] int? catagoryId = null)
        {
           var dat =  await _bookServices.GetAllAsync( authorId ,catagoryId);
           return Ok(dat);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dat = await _bookServices.GetAsync(id);
                return Ok(dat);

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            try
            {
                await _bookServices.CreateAsync(dto);
                 return StatusCode(201); ;

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });

            }




        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto updateDto)
        {
            try
            {
                await _bookServices.UpdateAsync(id,updateDto);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            try
            {
                await _bookServices.DeleteAsync(id);
                return Ok("Successfully deleted");

            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            try
            {
                await _bookServices.SoftDelete(id);
                return Ok("Successfully soft deleted");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
