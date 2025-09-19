using deneme_2.Database;
using deneme_2.DTOs.BookDtos;
using deneme_2.Models;
using deneme_2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace deneme_2.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly AppDbContext _appDb;

        public BookServices(AppDbContext appDb)
        {
            this._appDb = appDb;
        }

        public async Task CreateAsync(BookCreateDto dto)
        {
            if (!_appDb.Authors.Any(a => a.Id == dto.AuthorId))
            {
                throw new ArgumentException($"Author with Id {dto.AuthorId} does not exist.");
            }

            if (!_appDb.Catagories.Any(c => c.Id == dto.CatagoryId))
            {
                throw new ArgumentException($"Category with Id {dto.CatagoryId} does not exist.");
            }

            Book book = new Book
            {
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate,
                AuthorId = dto.AuthorId,

                CatagoryId = dto.CatagoryId
            };


            book.CreatedAt = DateTime.UtcNow.AddHours(4);
            book.UpdatedAt = DateTime.UtcNow.AddHours(4);
            book.IsDeleted = false;

            await _appDb.Books.AddAsync(book);
            await _appDb.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var bookToDelete = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (bookToDelete == null)
            {
                // Bu ID-li kitab tapılmadıqda xətanı dərhal atır
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }

            if (bookToDelete != null)
            {
                _appDb.Remove(bookToDelete);
                await _appDb.SaveChangesAsync();
               
            }

        }

        public async Task<List<BookGetDto>> GetAllAsync(int? authorId , int? catagoryId)
        {

            IQueryable<Book> query = _appDb.Books.Where(e => e.IsDeleted == false);
            if (authorId.HasValue)
            {
                query = query.Where(e => e.AuthorId == authorId.Value);
            }
            if (catagoryId.HasValue)
            {
                query = query.Where(e => e.CatagoryId == catagoryId.Value);
            }


            var data =  await query.Select(e => new BookGetDto
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
            
            return data;
        }

        public async Task<BookGetDto> GetAsync(int id)
        {
            var book = await _appDb.Books.FindAsync(id);

            if (book is null || book.IsDeleted == true)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }

            BookGetDto dto = new BookGetDto()
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

            return dto;
        }

        public async Task SoftDelete(int id)
        {
            var bookToSoftDelete = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (bookToSoftDelete == null)
            {
                // Bu ID-li kitab tapılmadıqda xətanı dərhal atır
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }

            if (bookToSoftDelete != null)
            {
                bookToSoftDelete.IsDeleted = true;
                await _appDb.SaveChangesAsync();
                
            }

            
        }

        public async Task UpdateAsync(int id, BookUpdateDto dto)
        {
            var bookToUpdate = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }


            if (!_appDb.Authors.Any(a => a.Id == dto.AuthorId))
            {
                throw new ArgumentException($"Author with Id {dto.AuthorId} does not exist.");
            }

            if (!_appDb.Catagories.Any(c => c.Id == dto.CatagoryId))
            {
                throw new ArgumentException($"Category with Id {dto.CatagoryId} does not exist.");
            }

            if (bookToUpdate != null)
            {
                bookToUpdate.Title = dto.Title;
                bookToUpdate.Price = dto.Price;
                bookToUpdate.AuthorId = dto.AuthorId;
                bookToUpdate.CatagoryId = dto.CatagoryId;
                bookToUpdate.Description = dto.Description;
                bookToUpdate.UpdatedAt = DateTime.UtcNow.AddHours(4);
                bookToUpdate.ReleaseDate = dto.ReleaseDate;




                await _appDb.SaveChangesAsync();
            }
        }
    }
}
