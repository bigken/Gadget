using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Gadget.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Gadget.Data
{
    public class GadgetDbContext : DbContext
    {
        public GadgetDbContext(DbContextOptions<GadgetDbContext> options) : base(options)
        {

        }
        
        public DbSet<Author> Authors { get; set; }
    }
}
