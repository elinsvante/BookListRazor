using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Model
{
    public class ApplicationDbContext : DbContext
    {       
        /// <summary>
        /// Empty constructor - paramater needed for dependency injection.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
    }
}
