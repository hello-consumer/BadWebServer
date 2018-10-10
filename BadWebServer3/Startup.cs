using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BadWebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("LOG: Configuring Services");

            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddRazorViewEngine();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {


            Console.WriteLine("LOG: Configure");

            //app.UseDeveloperExceptionPage();


            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

        }
    }
}
