using Greentube.Identity.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Greentube.Identity.Infrastructure
{
    public class GreentubeUserContext : IdentityDbContext
    {
        public GreentubeUserContext(DbContextOptions<GreentubeUserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserEntityConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
