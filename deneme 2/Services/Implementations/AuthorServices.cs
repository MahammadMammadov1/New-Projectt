using deneme_2.Database;
using deneme_2.DTOs.AuthorDtos;
using deneme_2.Exceptions;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace deneme_2.Services.Implementations
{
    public class AuthorServices : IAuthorServices
    {
        private readonly AppDbContext _appDb;

        public AuthorServices(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task CreateAsync(AuthorCreateDto dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                CreatedAt = DateTime.UtcNow.AddHours(4),
            };

            await _appDb.Authors.AddAsync(author);
            await _appDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _appDb.Authors.FirstOrDefaultAsync(e => e.Id == id);
            if (author == null)
                throw new NotFoundException("Author not found");

            _appDb.Authors.Remove(author);
            await _appDb.SaveChangesAsync();
        }

        public async Task<List<AuthorGetDto>> GetAllAsync()
        {
            var authors = await _appDb.Authors
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync();

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
            var author = await _appDb.Authors
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (author == null)
                throw new NotFoundException("Author not found");

            return new AuthorGetDto
            {
                Id = author.Id,
                Name = author.FirstName + " " + author.LastName,
                IsDeleted = author.IsDeleted,
                CreatedDate = author.CreatedAt,
                UpdatedDate = author.UpdatedAt,
                BirthDate = author.BirthDate
            };
        }

        public async Task SoftDelete(int id)
        {
            var author = await _appDb.Authors.FindAsync(id);
            if (author == null)
                throw new NotFoundException("Author not found");

            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AuthorUpdateDto dto)
        {
            var author = await _appDb.Authors.FindAsync(id);
            if (author == null)
                throw new NotFoundException("Author not found");

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            author.BirthDate = dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
        }
    }
}
