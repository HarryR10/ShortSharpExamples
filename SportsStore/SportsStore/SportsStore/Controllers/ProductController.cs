using System;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        // здесь, по сути, и происходит внедрение зависимости
        // в классе startup, в ConfigureServices() мы задали соответствие
        // для этого интерфейса
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Products);
    }
}