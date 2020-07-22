using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCRazorExample.Util
{
    public class ShowMoreIActions : IActionResult
    {
        string _sb = "На основе базового класса ActionResult в.NET Core создано " +
            "несколько классов, которые предназначены для разных типов " +
            "возвращаемых данных:";

        public ShowMoreIActions()
        {
            var sb = new StringBuilder();
            sb.Append("<ul>");

            using (var reader = new StreamReader("Files/IActionsList.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append($"<li>{line}</li>");
                }
            }
            sb.Append("</ul>");
            _sb += sb.ToString();
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            string fullHtmlCode = "<!DOCTYPE html><html><head>";
            fullHtmlCode += "<title>Главная страница</title>";
            fullHtmlCode += "<meta charset=utf-8 />";
            fullHtmlCode += "</head><body>";
            fullHtmlCode += $"{_sb}</body></html>";
            await context.HttpContext.Response.WriteAsync(fullHtmlCode);
        }
    }
}
