using System;
using Microsoft.AspNetCore.Mvc;
using Resourse.Models;

namespace Resourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly BookStore _store;

        public BooksController(BookStore store)
        {
            _store = store;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAvalibleBooks()
        {
            return Ok(_store.Books);
        }
    }
}
