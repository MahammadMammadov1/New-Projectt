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
        private readonly ILogger<AuthorServices> _logger;

        public AuthorServices(AppDbContext appDb,ILogger<AuthorServices> logger)
        {
            _appDb = appDb;
            _logger = logger;
        }

        public async Task CreateAsync(AuthorCreateDto dto)
        {
            _logger.LogInformation("Starting creation of a new author: {@Dto}", dto);

            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                CreatedAt = DateTime.UtcNow.AddHours(4),
            };
            _logger.LogInformation("Creating a new author: {FirstName} {LastName}", dto.FirstName, dto.LastName);

            await _appDb.Authors.AddAsync(author);
            await _appDb.SaveChangesAsync();
            _logger.LogInformation("Author {FirstName} {LastName} created successfully with Id {Id}", author.FirstName, author.LastName, author.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _appDb.Authors.FirstOrDefaultAsync(e => e.Id == id);
            _logger.LogInformation("Attempting to delete author with Id: {Id}", id);
            if (author == null)
            {
                _logger.LogWarning("Author with Id: {Id} not found", id);
                throw new NotFoundException("Author not found");

            }


            _appDb.Authors.Remove(author);
            await _appDb.SaveChangesAsync();

            _logger.LogInformation("Author with Id: {Id} deleted successfully", id);
        }

        public async Task<List<AuthorGetDto>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all authors");
            var authors = await _appDb.Authors
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .ToListAsync();
            _logger.LogInformation("Total authors retrieved: {Count}", authors.Count);


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
            _logger.LogInformation("Retrieving author with Id: {Id}", id);
            if (author == null)
            {
                _logger.LogWarning("Author with Id: {Id} not found", id);
                throw new NotFoundException("Author not found");

            }

            _logger.LogInformation("Author with Id: {Id} retrieved successfully", id);
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
            _logger.LogInformation("Attempting to soft delete author with Id: {Id}", id);
            if (author == null)
            {
                _logger.LogWarning("Author with Id: {Id} not found", id);
                throw new NotFoundException("Author not found");

            }

            author.IsDeleted = true;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
            _logger.LogInformation("Author with Id: {Id} soft deleted successfully", id);
        }

        public async Task UpdateAsync(int id, AuthorUpdateDto dto)
        {
            var author = await _appDb.Authors.FindAsync(id);
            _logger.LogInformation("Attempting to update author with Id: {Id}", id);
            if (author == null)
            {
                _logger.LogWarning("Author with Id: {Id} not found", id);
                throw new NotFoundException("Author not found");

            }

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            author.BirthDate = dto.BirthDate;
            author.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _appDb.SaveChangesAsync();
            _logger.LogInformation("Author with Id: {Id} updated successfully", id);
        }
    }
}
