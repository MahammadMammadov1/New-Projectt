using deneme_2.Database;
using deneme_2.DTOs.AuthorDtos;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deneme_2.Services.Implementations
{
    public class AuthorServices : IAuthorServices
    {
        private readonly AppDbContext _appDb;

        public AuthorServices(AppDbContext appDb)
        {
            this._appDb = appDb;
        }
        public async Task CreateAsync(AuthorCreateDto dto)
        {
            Author author1 = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                CreatedAt = DateTime.UtcNow.AddHours(4),
            };
            await _appDb.Authors.AddAsync(author1);
            await _appDb.SaveChangesAsync();
            
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _appDb.Authors.FirstOrDefaultAsync(e => e.Id == id);
            if (author is null) throw new Exception("Author not found");
            _appDb.Authors.Remove(author);
            await _appDb.SaveChangesAsync();
            
        }

        public async Task<List<AuthorGetDto>> GetAllAsync()
        {
            var authors = await _appDb.Authors.AsNoTracking().Where(e => e.IsDeleted == false).ToListAsync();
            
            return authors.Select(x => new AuthorGetDto
            {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                IsDeleted = x.IsDeleted,
                CreatedDate = x.CreatedAt,
                UpdatedDate = x.UpdatedAt,
                BirthDate = x.BirthDate
            }).ToList();
        }

        public async Task<AuthorGetDto> GetByIdAsync(int id)
        {
            var authors = await _appDb.Authors.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
            if (authors is null) throw new Exception("Author not found");

            return new AuthorGetDto
            {
                Id = authors.Id,
                Name = authors.FirstName + " " + authors.LastName,
                IsDeleted = authors.IsDeleted,
                CreatedDate = authors.CreatedAt,
                UpdatedDate = authors.UpdatedAt,
                BirthDate = authors.BirthDate
            };
        }

        public async Task SoftDelete(int id)
        {
            var author = await _appDb.Authors.FindAsync(id);
            if (author is null) throw new Exception("Author not found");
            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
            
        }

        public async Task UpdateAsync(int id, AuthorUpdateDto dto)
        {
            var author =await _appDb.Authors.FindAsync(id);
            if (author is null) throw new Exception("Author not found");
            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            author.BirthDate = dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
            
        }
    }
}
