using deneme_2.Database;
using deneme_2.DTOs.BookDtos;
using deneme_2.Exceptions;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deneme_2.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly AppDbContext _appDb;
        private readonly ILogger<BookServices> _logger;

        public BookServices(AppDbContext appDb,ILogger<BookServices> logger)
        {
            _appDb = appDb;
            _logger = logger;
        }

        public async Task CreateAsync(BookCreateDto dto)
        {
            _logger.LogInformation("Creating a new book with title: {Title}", dto.Title);

            if (!_appDb.Authors.Any(a => a.Id == dto.AuthorId))
            {
                _logger.LogWarning("Author with Id {AuthorId} does not exist.", dto.AuthorId);
                throw new NotFoundException($"Author with Id {dto.AuthorId} does not exist.");

            }

            if (!_appDb.Catagories.Any(c => c.Id == dto.CatagoryId))
            {
                _logger.LogWarning("Category with Id {CatagoryId} does not exist.", dto.CatagoryId);
                throw new NotFoundException($"Category with Id {dto.CatagoryId} does not exist.");

            }

            Book book = new()
            {
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                AuthorId = dto.AuthorId,
                CatagoryId = dto.CatagoryId,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                UpdatedAt = DateTime.UtcNow.AddHours(4),
                IsDeleted = false
            };

            await _appDb.Books.AddAsync(book);
            await _appDb.SaveChangesAsync();

            _logger.LogInformation("Book {Title} created successfully with Id {Id}", book.Title, book.Id);


        }

        public async Task DeleteAsync(int id)
        {
            var book = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);
            _logger.LogInformation("Attempting to delete book with Id: {Id}", id);

            if (book == null)
            {
                _logger.LogWarning("Book with Id: {Id} not found", id);
                throw new NotFoundException($"Book with Id {id} not found.");

            }

            _appDb.Books.Remove(book);
            await _appDb.SaveChangesAsync();

            _logger.LogInformation("Book with Id: {Id} deleted successfully", id);
        }

        public async Task<List<BookGetDto>> GetAllAsync(int? authorId, int? catagoryId)
        {
            IQueryable<Book> query = _appDb.Books.Where(e => !e.IsDeleted);

            if (authorId.HasValue)
                query = query.Where(e => e.AuthorId == authorId.Value);

            if (catagoryId.HasValue)
                query = query.Where(e => e.CatagoryId == catagoryId.Value);

            _logger.LogInformation("Fetching books with filters - AuthorId: {AuthorId}, CatagoryId: {CatagoryId}", authorId, catagoryId);  
            _logger.LogInformation("Total books found: {Count}", await query.CountAsync());

            return await query.Select(e => new BookGetDto
            {
                Id = e.Id,
                Name = e.Title,
                Description = e.Description,
                Price = e.Price,
                AuthorId = e.AuthorId,
                CatagoryId = e.CatagoryId,
                CreatedDate = e.CreatedAt,
                UpdatedDate = e.UpdatedAt,
                IsDeleted = e.IsDeleted
            }).ToListAsync();
        }

        public async Task<BookGetDto> GetAsync(int id)
        {

            _logger.LogInformation("Fetching book with Id: {Id}", id);

            var book = await _appDb.Books.FindAsync(id);

            if (book == null || book.IsDeleted)
            {
                _logger.LogWarning("Book with Id: {Id} not found or is deleted", id);
                throw new NotFoundException($"Book with Id {id} not found.");
            }

            _logger.LogInformation("Book with Id: {Id} fetched successfully", id);


            return new BookGetDto
            {
                Id = book.Id,
                Name = book.Title,
                Price = book.Price,
                CatagoryId = book.CatagoryId,
                AuthorId = book.AuthorId,
                Description = book.Description,
                IsDeleted = book.IsDeleted,
                CreatedDate = book.CreatedAt,
                UpdatedDate = book.UpdatedAt
            };
        }

        public async Task SoftDelete(int id)
        {
            var book = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);
            _logger.LogInformation("Attempting to soft delete book with Id: {Id}", id);

            if (book == null)
            {
                _logger.LogWarning("Book with Id: {Id} not found", id);
                throw new NotFoundException($"Book with Id {id} not found.");

            }

            book.IsDeleted = true;
            await _appDb.SaveChangesAsync();
            _logger.LogInformation("Book with Id: {Id} soft deleted successfully", id);
        }

        public async Task UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);
            _logger.LogInformation("Attempting to update book with Id: {Id}", id);
            if (book == null)
            {
                _logger.LogWarning("Book with Id: {Id} not found", id);
                throw new NotFoundException($"Book with Id {id} not found.");

            }

            if (!_appDb.Authors.Any(a => a.Id == dto.AuthorId))
            {
                _logger.LogWarning("Author with Id {AuthorId} does not exist.", dto.AuthorId);
                throw new NotFoundException($"Author with Id {dto.AuthorId} does not exist.");
            }

            if (!_appDb.Catagories.Any(c => c.Id == dto.CatagoryId))
            {

                _logger.LogWarning("Category with Id {CatagoryId} does not exist.", dto.CatagoryId);
                throw new NotFoundException($"Category with Id {dto.CatagoryId} does not exist.");
            }

            book.Title = dto.Title;
            book.Price = dto.Price;
            book.AuthorId = dto.AuthorId;
            book.CatagoryId = dto.CatagoryId;
            book.Description = dto.Description;
            book.ReleaseDate = dto.ReleaseDate;
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDb.SaveChangesAsync();

            _logger.LogInformation("Book with Id: {Id} updated successfully", id);
        }
    }
}
