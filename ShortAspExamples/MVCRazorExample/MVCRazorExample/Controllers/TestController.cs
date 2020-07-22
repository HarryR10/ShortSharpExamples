using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCRazorExample.Util;

namespace MVCRazorExample.Controllers
{
    public class TestController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return new HtmlResult("<h2>it works</h2>");
            //return View();
            //если оставить View() в рантайме будем ловить ошибку:
            //The view 'Index' was not found. The following locations were searched... далее перечисление директорий
            //т.е. система будет пытаться найти файл .cshtml
        }
    }
}
