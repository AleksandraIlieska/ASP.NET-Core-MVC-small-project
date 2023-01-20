using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext   //Inheriting from class DbContext from Entiry Framework Core
    {
        // In the constructor we will recieve some options who are passed to the base class (DbContext)  -> general setup
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Creating category table in the database with name Categories
        public DbSet<Category> Categories { get; set; }
    }
}
