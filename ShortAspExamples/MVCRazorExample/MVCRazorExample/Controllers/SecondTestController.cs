using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCRazorExample.Util;

namespace MVCRazorExample.Controllers
{
    [Route("second")]
    public class SecondTestController : Controller
    {
        //т.е. https://localhost:5001/second/show
        [Route("show")]
        public IActionResult Index()
        {            
            return Content("Test");
        }

        //т.е. https://localhost:5001/second/details/1
        //где 1 - это id
        //{id?} - необязательный
        [Route("details/{id?}")]
        public string Details(string id)
        {
            //мы можем возвращать строку, но это не желательно
            return "ID Value = " + id;
        }

        //т.е. https://localhost:5001/second/more
        [Route("more")]
        public IActionResult ShowMore()
        {
            return new ShowMoreIActions();
        }

        //т.е. https://localhost:5001/second/context
        [Route("context")]
        public IActionResult ShowContext()
        {
            //обращение к контексту контроллера
            
            var user = ControllerContext.HttpContext.User;
            //либо обращение к контексту this.HttpContext; для последующей обработки

            return Content(user.ToString());
            //только пример, ожидаемо выведет System.Security.Claims.ClaimsPrincipal

            //еще возможны:
            //return this.NotFound();
            //return this.BadRequest("Something is really bad");
        }

        //т.е. https://localhost:5001/second/file
        [Route("file")]
        public IActionResult GetFile()
        {
            //1. загрузка происходит относительно папки wwwroot
            //2. "text/plain" не подходящий тип для этого файла, но на загрузку это не влияет
            return this.File("favicon.ico", "text/plain");
        }

        //т.е. https://localhost:5001/second/person
        [Route("person")]
        public Person[] ShowClass()
        {
            Person[] result = new Person[3];
            for(int i = 0; i < 3; i++)
            {
                result[i] = new Person { FirstName = $"Ivan{i}", LastName = "Ivanov" };
            }
            return result;
            //в результате мы получим JSON строку
        }

        //т.е. https://localhost:5001/second/person2
        [Route("person2")]
        public IActionResult ShowObjResult()
        {
            Person[] result = new Person[3];
            for (int i = 0; i < 3; i++)
            {
                result[i] = new Person { FirstName = $"Ivan{i}", LastName = "Ivanov" };
            }
            return new ObjectResult(result);
            //правильный вариант получения объекта 
        }
    }
}
