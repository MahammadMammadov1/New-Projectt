using deneme_2.DTOs.AuthorDtos;

namespace deneme_2.Services.Interfaces
{
    public interface IAuthorServices 
    {
        public Task CreateAsync(AuthorCreateDto dto);
        public Task UpdateAsync(int id, AuthorUpdateDto dto);
        public Task DeleteAsync(int id);
        public Task SoftDelete(int id);
        public Task<List<AuthorGetDto>> GetAllAsync();
        public Task<AuthorGetDto> GetByIdAsync(int id);
    }
}
