using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                .AddRazorViewEngine()
                .AddAuthorization();




            //For every request made to this app that requires XmlDataContext, MVC will automatically "New" up the XmlDataContext;
            //services.AddTransient<XmlDataContext>();

            //Using Microsoft.EntityFrameworkCore
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("BadDatabase"));


            //services.AddTransient<XmlUserManager>();

            //using Microsoft.AspNetCore.Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<Data.ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.Configure<Microsoft.AspNetCore.Identity.IdentityOptions>(options =>
            {
    // Password settings.
    options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {


            Console.WriteLine("LOG: Configure");

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

        }
    }
}
