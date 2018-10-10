using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BadWebServer
{
    public class Program
    {
        static void Main(string[] args)
        { 
        
            var builder = WebHost.CreateDefaultBuilder(args);
            var webHost = builder
                .UseContentRoot(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.ToString())
                .UseStartup<Startup>()
                .Build();
            webHost.Run();
        }
        
    }
}
