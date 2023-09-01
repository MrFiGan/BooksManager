using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BooksAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDBContext _dbContext;
        public BookRepository(BooksDBContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task DeleteAsync(Book entity)
        {
            _dbContext.Books.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetAsync(Guid id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IEnumerable<Book>> GetBooksByPaginationAndSorting(int pageNumber, int pageSize, string searchText, string sortBy, bool ascending)
        {
            return await _dbContext.Books
                .FromSqlRaw("EXEC GetBooksByPaginationAndSorting {0}, {1}, {2}, {3}, {4}", pageNumber, pageSize, searchText, sortBy, ascending)
                .ToListAsync();
        }

        public async Task<Book> PostAsync(Book newBook)
        {
            _dbContext.Books.Add(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook;
        }

        public async Task<Book> PutAsync(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return book;
        }
    }
}