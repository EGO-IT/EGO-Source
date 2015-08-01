using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EGO.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("EGOConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Client> Clients { get; set; }
    }
}