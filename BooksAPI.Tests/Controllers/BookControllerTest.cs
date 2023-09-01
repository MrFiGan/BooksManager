using BooksAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Moq;
using BooksAPI.Repositories;
using BooksAPI.Models;
using System.Linq;
using System.Web.Http.Results;

namespace BooksAPI.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private Mock<IBookRepository> _mockBookRepository;

        private BooksController _booksController;

        [TestInitialize]
        public void TestInitialize()
        {
            IEnumerable<Book> books = new[] {
                new Book()
                {
                   Id = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea"),
                   Title = "Title 1",
                   Author = "Author 1",
                   ISBN = "9.9",
                   PublicationYear = 1965,
                   Quantity = 25,
                   CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")
                },
                new Book()
                {
                   Id = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8e2"),
                   Title = "Title 2",
                   Author = "Author 2",
                   ISBN = "9.9",
                   PublicationYear = 1965,
                   Quantity = 25,
                   CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")
                },
                new Book()
                {
                   Id = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8e3"),
                   Title = "Title 3",
                   Author = "Author 3",
                   ISBN = "9.9",
                   PublicationYear = 1965,
                   Quantity = 25,
                   CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")
                },
             };

            _mockBookRepository = new Mock<IBookRepository>();

            _mockBookRepository.Setup(x => x.GetAsync()).Returns(Task.FromResult(books));
            _mockBookRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult(books.First()));
            _mockBookRepository.Setup(x => x.PostAsync(It.IsAny<Book>())).Returns(Task.FromResult(books.First()));
            _mockBookRepository.Setup(x => x.PutAsync(It.IsAny<Book>())).Returns(Task.FromResult(books.First()));
            _mockBookRepository.Setup(x => x.DeleteAsync(It.IsAny<Book>())).Returns(Task.CompletedTask);
            _mockBookRepository.Setup(x => x.GetBooksByPaginationAndSorting(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(books));

            _booksController = new BooksController(_mockBookRepository.Object);
        }

        [TestMethod]
        public async Task GetBooks()
        {
            var result = await _booksController.GetBooks();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<Book>>));

            var value = ((OkNegotiatedContentResult<IEnumerable<Book>>)result).Content;
            Assert.AreEqual(value.Count(), 3);
            Assert.AreEqual(value.First().Author, "Author 1");
        }

        [TestMethod]
        public async Task GetBook()
        {
            var guid = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea");
            var result = await _booksController.GetBook(guid);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Book>));

            var value = ((OkNegotiatedContentResult<Book>)result).Content;
            Assert.AreEqual(value.Author, "Author 1");
            Assert.AreEqual(value.Title, "Title 1");
            Assert.AreEqual(value.PublicationYear, 1965);
        }

        [TestMethod]
        public async Task GetBook_NotFound()
        {
            _mockBookRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Book)null));

            var guid = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea");
            var result = await _booksController.GetBook(guid);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateBook()
        {
            var user = new BookViewModel()
            {
                Title = "Title 3",
                Author = "Author 3",
                ISBN = "9.9",
                PublicationYear = 1965,
                Quantity = 25,
                CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")

            };
            var result = await _booksController.PostBook(user);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Book>));

            var value = ((OkNegotiatedContentResult<Book>)result).Content;
            Assert.AreEqual(value.Author, "Author 3");
            Assert.AreEqual(value.Title, "Title 3");
            Assert.AreEqual(value.PublicationYear, 1965);
        }

        [TestMethod]
        public async Task UpdateBook()
        {
            var user = new BookViewModel()
            {
                Title = "Title 3",
                Author = "Author 3",
                ISBN = "9.9",
                PublicationYear = 1965,
                Quantity = 25,
                CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")

            };
            var result = await _booksController.PutBook(Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea"), user);

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<Book>));

            var value = ((OkNegotiatedContentResult<Book>)result).Content;
            Assert.AreEqual(value.Author, "Author 3");
            Assert.AreEqual(value.Title, "Title 3");
            Assert.AreEqual(value.PublicationYear, 1965);
        }

        [TestMethod]
        public async Task UpdateBook_NotFound()
        {
            _mockBookRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Book)null));

            var user = new BookViewModel()
            {
                Title = "Title 3",
                Author = "Author 3",
                ISBN = "9.9",
                PublicationYear = 1965,
                Quantity = 25,
                CategoryId = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f")

            };
            var result = await _booksController.PutBook(Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea"), user);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteBook()
        {
            var guid = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea");
            var result = await _booksController.DeleteBook(guid);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteBook_NotFound()
        {
            _mockBookRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Returns(Task.FromResult((Book)null));

            var guid = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea");
            var result = await _booksController.DeleteBook(guid);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
