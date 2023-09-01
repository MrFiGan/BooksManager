using BooksAPI.Controllers;
using BooksAPI.Models;
using BooksAPI.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace BooksAPI.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private Mock<ICategoryRepository> _mockCategoryRepository;

        private CategoriesController _categoriesController;

        [TestInitialize]
        public void TestInitialize()
        {
            IEnumerable<Category> books = new[] {
                new Category()
                {
                   Id = Guid.Parse("c6a6faea-7a1e-4b15-a783-d9dbe439a8ea"),
                   Name = "Test 1",
                   Description = "Description 1"
                },
                new Category()
                {
                   Id = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f"),
                   Name = "Test 2",
                   Description = "Description 2"
                },
                new Category()
                {
                   Id = Guid.Parse("f2e0a9df-607f-4fec-8a27-0e917f6cd76f"),
                   Name = "Test 3",
                   Description = "Description 3"
                },
             };

            _mockCategoryRepository = new Mock<ICategoryRepository>();

            _mockCategoryRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(books));

            _categoriesController = new CategoriesController(_mockCategoryRepository.Object);
        }

        [TestMethod]
        public async Task GetBooks()
        {
            var result = await _categoriesController.GetCategories();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<Category>>));

            var value = ((OkNegotiatedContentResult<IEnumerable<Category>>)result).Content;
            Assert.AreEqual(value.Count(), 3);
            Assert.AreEqual(value.First().Name, "Test 1");
        }
    }
}
