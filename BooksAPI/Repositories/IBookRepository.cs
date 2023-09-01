using BooksAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAPI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAsync(); 
        Task<IEnumerable<Book>> GetBooksByPaginationAndSorting(int pageNumber, int pageSize, string searchText, string sortBy, bool ascending);
        Task<Book> GetAsync(Guid id);
        Task<Book> PostAsync(Book newBook);
        Task<Book> PutAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
