using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resourse.Models;

namespace Resourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly BookStore _store;

        private Guid UserId => Guid.Parse(User.Claims.Single(c =>
            c.Type == ClaimTypes.NameIdentifier).Value);

        public OrdersController(BookStore store)
        {
            _store = store;
        }

        [HttpGet]
        [Authorize (Roles = "User")]
        [Route("")]
        public IActionResult GetOrders()
        {
            if (!_store.Orders.ContainsKey(UserId))
            {
                return Ok(Enumerable.Empty<Book>());
            }

            var orderedBooksIds = _store.Orders.Single(o => o.Key == UserId).Value;
            var orderedBooks = _store.Books.Where(b => orderedBooksIds.Contains(b.Id));

            return Ok(orderedBooks);
        }
    }
}
