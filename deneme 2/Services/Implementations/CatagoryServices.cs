using deneme_2.Database;
using deneme_2.DTOs.CatagoryDtos;
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
    public class CatagoryServices : ICatagoryIServices
    {
        private readonly AppDbContext _appDb;

        public CatagoryServices(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task CreateAsync(CatagoryCreateDto dto)
        {
            if (await _appDb.Catagories.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower()))
                throw new ConflictException("Category with the same name already exists.");

            var catagory = new Catagory
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4)
            };

            await _appDb.Catagories.AddAsync(catagory);
            await _appDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var catagory = await _appDb.Catagories.FindAsync(id);
            if (catagory == null)
                throw new NotFoundException("Category not found");

            _appDb.Catagories.Remove(catagory);
            await _appDb.SaveChangesAsync();
        }

        public async Task SoftDelete(int id)
        {
            var catagory = await _appDb.Catagories.FindAsync(id);
            if (catagory == null)
                throw new NotFoundException("Category not found");

            catagory.IsDeleted = true;
            await _appDb.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CatagoryUpdateDto dto)
        {
            var catagory = await _appDb.Catagories
                                       .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (catagory == null)
                throw new NotFoundException("Category not found or has been deleted.");

            catagory.Name = dto.Name;
            catagory.Description = dto.Description;
            catagory.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDb.SaveChangesAsync();
        }

        public async Task<List<CatagoryGetDto>> GetAllAsync()
        {
            return await _appDb.Catagories
                .Where(c => !c.IsDeleted)
                .Select(c => new CatagoryGetDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedAt,
                    UpdatedDate = c.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<CatagoryGetDto> GetAsync(int id)
        {
            var catagory = await _appDb.Catagories
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (catagory == null)
                throw new NotFoundException("Category not found");

            return new CatagoryGetDto
            {
                Id = catagory.Id,
                Name = catagory.Name,
                Description = catagory.Description,
                CreatedDate = catagory.CreatedAt,
                UpdatedDate = catagory.UpdatedAt
            };
        }
    }
}
