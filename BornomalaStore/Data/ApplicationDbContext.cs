using BornomalaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BornomalaStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //create connection with entity framework core
        {

        }
        public DbSet<Book> Books { get; set; }
        
    }
}
