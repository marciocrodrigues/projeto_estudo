using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoParaEstudo.Domain.Entities;

namespace ProjetoParaEstudo.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(250);

            builder.ToTable("Users");
        }
    }
}
