using deneme_2.Database;
using deneme_2.DTOs.AccountDtos;
using deneme_2.Exceptions;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace deneme_2.Services.Implementations
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDbContext _appDb;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountServices> _logger;

        public AccountServices(AppDbContext appDb,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<AccountServices> logger)

        {
            _appDb = appDb;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        


        public async Task LoginAsync(LoginDto loginDto)
        {
            AppUser appUser = null;

            appUser = await _userManager.FindByNameAsync(loginDto.UserName_or_Email);

            if(appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(loginDto.UserName_or_Email);

                if(appUser == null)
                {
                    _logger.LogWarning("Login failed: User with username or email {UserName_or_Email} not found",
                                       loginDto.UserName_or_Email);
                    throw new CredentialException("Username or Email is incorrect");
                }
            }
            if (!await _userManager.CheckPasswordAsync(appUser, loginDto.Password))
            {
                _logger.LogWarning("Login failed: Incorrect password for user {UserName_or_Email}",
                                   loginDto.UserName_or_Email);
                throw new CredentialException("Password or Gmail or Username is incorrect");
            }
            _logger.LogInformation("User {UserName_or_Email} logged in successfully", loginDto.UserName_or_Email);

        }

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            _logger.LogInformation("Registering a new user with username: {Username} and email: {Email}",
                                   registerDto.UserName, registerDto.Email);
            if (await _userManager.FindByNameAsync(registerDto.UserName) != null)
                throw new CredentialException("Username is already taken");

            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                throw new CredentialException("Email is already taken");

            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new CredentialException("Password and Confirm Password do not match");

            var appUser = new AppUser
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            _logger.LogInformation("Created AppUser object for username: {Username}", appUser.UserName);
            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new CredentialException($"User creation failed: {errors}");

            }

            await _userManager.AddToRoleAsync(appUser, "Member");
            _logger.LogInformation("User {Username} registered successfully with email: {Email}",
                                   appUser.UserName, appUser.Email);
        }

    }
}
