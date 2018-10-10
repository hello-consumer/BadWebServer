using System;
using System.Collections.Generic;
using System.Text;

namespace BadWebServer.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private static List<string> myList = new List<string>();

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return Content(string.Join(',', myList.ToArray()));
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(string name)
        {
            myList.Add(name);
            return Created("/home/", name);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(string name, string newName)
        {
            int index = myList.IndexOf(name);
            myList.RemoveAt(index);
            myList.Insert(index, newName);
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(string name, bool? extra)
        {
            myList.Remove(name);
            return Ok();
        }
    }
}
