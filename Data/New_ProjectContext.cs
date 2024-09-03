using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Models;

namespace New_Project.Data
{
    public class New_ProjectContext : DbContext
    {
        public New_ProjectContext (DbContextOptions<New_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movies> Movie { get; set; } = default!;
    }

  
}
