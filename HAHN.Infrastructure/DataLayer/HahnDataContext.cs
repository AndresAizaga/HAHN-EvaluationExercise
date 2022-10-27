using HAHN.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HAHN.Infrastructure.DataLayer
{
    public partial class HahnDataContext : DbContext
    {
        public DbSet<ContactModel> Contact { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=Hahn;Trusted_Connection=True");
        }
    }
}
