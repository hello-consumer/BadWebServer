
using System;
using System.Collections.Generic;
using System.Linq;

//Manage NuGet Packages: Install Microsoft.AspNetCore
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//Manage NuGet Packages: Install Microsoft.AspNetCore.Mvc

namespace BadWebServer
{
    
}



// private static List<string> myList = new List<string>();
//private static void Listen()
//{
//    http.BeginGetContext((iar) =>
//    {
//        var context = http.EndGetContext(iar);
//        System.IO.Stream stream = context.Response.OutputStream;
//        byte[] encodedMessage;

//        switch (context.Request.HttpMethod)
//        {
//            case "GET":

//                encodedMessage = System.Text.Encoding.ASCII.GetBytes(string.Join(',', myList.ToArray()));
//                stream.Write(encodedMessage, 0, encodedMessage.Length);
//                context.Response.Close();
//                Console.WriteLine("GET called");
//                break;
//            case "POST":
//                if (context.Request.QueryString.AllKeys.Contains("name") && context.Request.QueryString.AllKeys.Contains("newName"))
//                {
//                    string name = context.Request.QueryString["name"];
//                    string newName = context.Request.QueryString["newName"];
//                    int index = myList.IndexOf(name);
//                    myList.RemoveAt(index);
//                    myList.Insert(index, newName);
//                    encodedMessage = System.Text.Encoding.ASCII.GetBytes("Updated");
//                    stream.Write(encodedMessage, 0, encodedMessage.Length);
//                    context.Response.Close();
//                }
//                Console.WriteLine("POST called");
//                break;
//            case "PUT":
//                if (context.Request.QueryString.AllKeys.Contains("name"))
//                {
//                    string name = context.Request.QueryString["name"];
//                    myList.Add(name);

//                    encodedMessage = System.Text.Encoding.ASCII.GetBytes("Created");
//                    stream.Write(encodedMessage, 0, encodedMessage.Length);
//                    context.Response.Close();
//                }
//                Console.WriteLine("PUT called");
//                break;
//            case "DELETE":
//                if (context.Request.QueryString.AllKeys.Contains("name"))
//                {
//                    string name = context.Request.QueryString["name"];
//                    myList.Remove(name);

//                    encodedMessage = System.Text.Encoding.ASCII.GetBytes("Deleted");
//                    stream.Write(encodedMessage, 0, encodedMessage.Length);
//                    context.Response.Close();
//                }
//                Console.WriteLine("DELETE called");
//                break;
//        }

//        Listen();
//    }, new object());
//}
