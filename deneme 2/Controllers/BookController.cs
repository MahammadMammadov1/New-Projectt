using deneme_2.DTOs.BookDtos;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] int? authorId = null, [FromQuery] int? catagoryId = null)
        {
            var data = await _bookServices.GetAllAsync(authorId, catagoryId);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _bookServices.GetAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateDto dto)
        {
            await _bookServices.CreateAsync(dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookUpdateDto dto)
        {
            await _bookServices.UpdateAsync(id, dto);
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDelete(int id)
        {
            await _bookServices.DeleteAsync(id);
            return Ok("Successfully deleted");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _bookServices.SoftDelete(id);
            return Ok("Successfully soft deleted");
        }
    }
}
