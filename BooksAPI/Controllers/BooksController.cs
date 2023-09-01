using BooksAPI.Models;
using BooksAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BooksAPI.Controllers
{
    public class BooksController : ApiController
    {
        private readonly IBookRepository _repository;
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBooks(int pageNumber = 1, int pageSize = int.MaxValue, string searchText = "", string sortBy = "Title", bool ascending = true)
        {
            var books = await _repository.GetBooksByPaginationAndSorting(pageNumber, pageSize, searchText, sortBy, ascending);

            return Ok(books);
        }

        [HttpGet]
        [Route("api/books/{id}")]
        public async Task<IHttpActionResult> GetBook(Guid id)
        {
            var book = await _repository.GetAsync(id);

            if (book == null)
            {
                return NotFound(); // Return 404 if the book is not found
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostBook(BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newBook = new Book(viewModel);

            await _repository.PostAsync(newBook);

            return Ok(newBook);
        }

        [HttpPut]
        [Route("api/books/{id}")]
        public async Task<IHttpActionResult> PutBook(Guid id, BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBook = await _repository.GetAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            // Update properties with data from ViewModel
            existingBook.Title = viewModel.Title;
            existingBook.Author = viewModel.Author;
            existingBook.ISBN = viewModel.ISBN;
            existingBook.PublicationYear = viewModel.PublicationYear;
            existingBook.Quantity = viewModel.Quantity;
            existingBook.CategoryId = viewModel.CategoryId;

            await _repository.PutAsync(existingBook);

            return Ok(existingBook);
        }

        [HttpDelete]
        [Route("api/books/{id}")]
        public async Task<IHttpActionResult> DeleteBook(Guid id)
        {
            var book = await _repository.GetAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(book);

            return Ok(); 
        }
    }
}
