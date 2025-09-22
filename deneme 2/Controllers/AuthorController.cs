using deneme_2.Database;
using deneme_2.DTOs.AuthorDtos;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
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
        private readonly IAuthorServices _authorServices;

        public AuthorController(AppDbContext appDb,IAuthorServices authorServices)
        {
            this._appDb = appDb;
            this._authorServices = authorServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            try
            {
                var dat = await _authorServices.GetByIdAsync(id);
                return Ok(dat);
            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var dat =await _authorServices.GetAllAsync();
                return Ok(dat);
            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorCreateDto author)
        {
            await _authorServices.CreateAsync(author);
            return Ok("Succesfuly");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorUpdateDto dto)
        {
            try
            {
                await _authorServices.UpdateAsync(id, dto);
                return Ok("Succesfuly");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeleteAuthor(int id)
        {
            try
            {
                await _authorServices.DeleteAsync(id);
                return Ok("Succesfuly");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
           
        }

        [HttpPatch("{id}/softdelete")]
        public async Task<IActionResult> SoftDeleteAuthor(int id)
        {
            try
            {
                await _authorServices.SoftDelete(id);
                return Ok("Succesfuly");
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });

            }
        }
    }
}
