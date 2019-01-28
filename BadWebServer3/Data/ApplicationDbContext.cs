using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Data
{
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<Microsoft.AspNetCore.Identity.IdentityUser>
    {
        public ApplicationDbContext() : base()
        {

        }

        public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<Item> Items { get; set; }
    }

    public class Item
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
}
