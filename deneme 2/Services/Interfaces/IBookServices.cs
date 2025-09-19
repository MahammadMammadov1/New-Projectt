namespace deneme_2.Services.Interfaces
{
    public interface IBookServices 
    {
        public Task<List<DTOs.BookDtos.BookGetDto>> GetAllAsync(int? authorId, int? catagoryId);
        public Task<DTOs.BookDtos.BookGetDto> GetAsync(int id);
        public Task CreateAsync(DTOs.BookDtos.BookCreateDto dto);
        public Task UpdateAsync(int id, DTOs.BookDtos.BookUpdateDto dto);
        public Task DeleteAsync(int id);
        public Task SoftDelete(int id);
    }
}
