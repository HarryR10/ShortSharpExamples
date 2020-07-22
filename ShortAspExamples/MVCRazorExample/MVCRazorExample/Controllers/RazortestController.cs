using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCRazorExample.Controllers
{
    public class RazortestController : Controller
    {
        //т.е. по адресу https://localhost:5001/razortest
        //Index в запрос можно не включать
        public IActionResult Index()
        {
            Person person = new Person { FirstName = "Ivan", LastName = "Ivanov" };

            return View(person);
            //return View("Test"); например для Test.cshtml
        }
    }
}
