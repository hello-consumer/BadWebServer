using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly string filename = "users.xml";

        public Microsoft.AspNetCore.Mvc.IActionResult Register()
        {
            return View();
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult Register(Models.User model)
        {
            bool fileExists = System.IO.File.Exists(filename);
            bool userRegistered = false;

            //On Posts, I use the ModelState.IsValid to only run my logic if the user posts a valid message.
            if (ModelState.IsValid) 
            {
                using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
                    List<Models.User> users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();

                    //No matter what, by the time I get here, I have a list of users to work with!
                    if (!users.Any(x => x.Username == model.Username))
                    {
                        users.Add(model);
                        userRegistered = true;
                        //Every subsequent request will contain this cookie until it "expires"... I can set the expiration date in the Cookie Options
                        Response.Cookies.Append("SignedInUsername", model.Username);
                    }
                    else
                    {
                        //Model State is a friendlier way of displaying errors after the user posts info to the server.
                        ModelState.AddModelError("Username", "Username is already taken");
                    }


                    if (fileExists)
                    {
                        stream.Position = 0;
                    }
                    serializer.Serialize(stream, users);
                }

            }
            if (userRegistered)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        public Microsoft.AspNetCore.Mvc.IActionResult SignIn()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public Microsoft.AspNetCore.Mvc.IActionResult SignIn(Models.User model)
        {
            bool signedIn = false;
            //On Posts, I use the ModelState.IsValid to only run my logic if the user posts a valid message.
            if (ModelState.IsValid && System.IO.File.Exists(filename))
            {
                using (var stream = System.IO.File.Open(filename, System.IO.FileMode.Open))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
                    List<Models.User> users = (List<Models.User>)serializer.Deserialize(stream);

                    
                    //No matter what, by the time I get here, I have a list of users to work with!
                    if (users.Any(x => x.Username == model.Username) && users.First(x => x.Username == model.Username).Password == model.Password)
                    {
                        //Every subsequent request will contain this cookie until it "expires"... I can set the expiration date in the Cookie Options
                        Response.Cookies.Append("SignedInUsername", model.Username);
                        signedIn = true;
                    }
                    else
                    {
                        
                        ModelState.AddModelError("Username", "Email Address or Password are invalid");
                    }
                }

            }
            if (signedIn)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        public Microsoft.AspNetCore.Mvc.IActionResult SignOut()
        {
            Response.Cookies.Delete("SignedInUsername");
            return RedirectToAction("Index", "Home");
        }


    }
}
