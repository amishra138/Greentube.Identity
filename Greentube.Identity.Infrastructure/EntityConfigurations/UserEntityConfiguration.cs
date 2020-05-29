using Greentube.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Greentube.Identity.Infrastructure.EntityConfigurations
{
    /// <summary>
    /// It allows configuration for an entity into a seperate class
    /// </summary>
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(_ => _.Id);
        }
    }
}
