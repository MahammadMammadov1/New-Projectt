namespace deneme_2.Services.Interfaces
{
    public interface IAccountServices 
    {
        Task RegisterAsync(DTOs.AccountDtos.RegisterDto registerDto);
        Task LoginAsync(DTOs.AccountDtos.LoginDto loginDto);
        
    }
}
