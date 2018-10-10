using System;
using System.Collections.Generic;
using System.Text;

namespace BadWebServer.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly string filename = "data.xml";
        
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            List<string> myList = new List<string>();

            if (System.IO.File.Exists(filename))
            {

                using (var stream = System.IO.File.OpenRead(filename))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    myList = (List<string>)serializer.Deserialize(stream);
                    
                }
            }
            return View(myList);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Microsoft.AspNetCore.Mvc.IActionResult Create(string name)
        {
            if (System.IO.File.Exists(filename))
            {

                using (var stream = System.IO.File.Open(filename, System.IO.FileMode.Open))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    List<string> myList = (List<string>)serializer.Deserialize(stream);
                    myList.Add(name);
                    stream.Position = 0;
                    serializer.Serialize(stream, myList);
                    
                }
            }
            else
            {
                using (var stream = System.IO.File.Create(filename))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    List<string> myList = new List<string>();
                    myList.Add(name);
                    serializer.Serialize(stream, myList);
                }
            }
            return RedirectToAction("Index");
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Microsoft.AspNetCore.Mvc.IActionResult Delete(string name)
        {
            if (System.IO.File.Exists(filename))
            {

                using (var stream = System.IO.File.Open(filename, System.IO.FileMode.Open))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    List<string> myList = (List<string>)serializer.Deserialize(stream);
                    stream.SetLength(0);
                    myList.Remove(name);
                    stream.Position = 0;
                    serializer.Serialize(stream, myList);

                }
            }
            else
            {
                using (var stream = System.IO.File.Create(filename))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    List<string> myList = new List<string>();
                    serializer.Serialize(stream, myList);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
