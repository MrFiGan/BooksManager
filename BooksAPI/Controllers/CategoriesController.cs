using BooksAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BooksAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoryRepository _repository;
        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCategories()
        {
            var categories = await _repository.GetAllAsync();

            return Ok(categories);
        }
    }
}