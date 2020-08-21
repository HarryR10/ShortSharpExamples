using System;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize = 4;

        public ProductController(IProductRepository repo)
        // здесь, по сути, и происходит внедрение зависимости
        // в классе startup, в ConfigureServices() мы задали соответствие
        // для этого интерфейса
        {
            repository = repo;
        }

        //https://localhost:5001/?productPage=2
        //https://localhost:5001/?category=Soccer
        //
        public ViewResult List(string category, int productPage = 1)
        {
            var products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize);

            return View(new ProductsListViewModel
            {
                Products = products.Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                },

                CurrentCategory = category
            });
        }
    }
}