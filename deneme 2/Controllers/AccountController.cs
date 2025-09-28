using deneme_2.DTOs.AccountDtos;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deneme_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            await _accountServices.RegisterAsync(registerDto);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _accountServices.LoginAsync(loginDto);

            return Ok(new
            {
                message = "User logged in successfully",
                token = token
            });
        }
    }
}
