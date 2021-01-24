using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeonMonolith.Models;

namespace NeonMonolith.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }
        internal DbSet<ResidentialProperty> ResidentialProperties { get; set; } = null!;
        internal DbSet<ResidentialLease> ResidentialLeases { get; set; } = null!;
    }
}
