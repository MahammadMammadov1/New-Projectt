using deneme_2.DTOs.BookDtos;
using deneme_2.DTOs.CatagoryDtos;

namespace deneme_2.Services.Interfaces
{
    public interface ICatagoryIServices 
    {
        public Task<List<CatagoryGetDto>> GetAllAsync();
        public Task<CatagoryGetDto> GetAsync(int id);
        public Task CreateAsync(CatagoryCreateDto dto);
        public Task UpdateAsync(int id, CatagoryUpdateDto dto);
        public Task DeleteAsync(int id);
        public Task SoftDelete(int id);
    }
}
