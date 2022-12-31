using Infrastructure.Persistence.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.MSSQL.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DateOfBirth).IsRequired().HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(13);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.OwnsOne(x => x.BankAccount, ba =>
            {
                ba.WithOwner(x => x.Customer).HasForeignKey("CustomerId");
                ba.Property<long>(x => x.CustomerId).HasColumnName("CustomerId");
                ba.Property(x => x.Number).IsRequired().HasMaxLength(10).HasColumnType("varchar(10)");
                ba.ToTable("BankAccount");
            });
            builder.OwnsOne(x => x.CustomerCreatedEvent, cce =>
            {
                cce.WithOwner(x => x.Customer).HasForeignKey("CustomerId");
                cce.Property<long>(x => x.CustomerId).HasColumnName("CustomerId");
                cce.Property(x => x.BankAccountNumber).IsRequired().HasMaxLength(10).HasColumnType("varchar(10)");
                cce.Property(x => x.DateTime).IsRequired().HasColumnType("datetimeoffset(7)");
                cce.Property(x => x.IsPublished).IsRequired(false).HasColumnType("bit");
                cce.Property(x => x.IsCallbackCompleted).IsRequired(false).HasColumnType("bit");
                cce.ToTable("CustomerCreatedEvent");
            });
        }
    }
}
