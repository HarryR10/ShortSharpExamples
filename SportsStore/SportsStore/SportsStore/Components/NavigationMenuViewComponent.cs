using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStore.Models;

namespace SportsStore.Components
{

    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        //опять же, передаем зависимость в метод (из Startup.cs):
        //нейминг - обращается к /Views/Shared/Components/NavigationMenu/Default.cshtml
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }

        //!!!
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}