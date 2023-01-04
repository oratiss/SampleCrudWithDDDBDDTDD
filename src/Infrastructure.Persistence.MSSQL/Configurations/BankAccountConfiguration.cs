using Infrastructure.Persistence.MSSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.MSSQL.Configurations
{
    public class BankAccountConfiguration: IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccount", "Financial");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AccountNumber).IsRequired().HasMaxLength(10).HasColumnType("varchar(10)");
            builder.Property(x => x.CreateDateTime).IsRequired().HasColumnType("datetimeoffset(7)");
            builder.Property(x => x.UpdateDateTime).IsRequired(false).HasColumnType("datetimeoffset(7)");

            builder.OwnsOne(x => x.CustomerVoForBankAccount, c =>
            {
                c.WithOwner(x => x.BankAccount).HasForeignKey("BankAccountId");
                c.Property<long>(x => x.BankAccountId).HasColumnName("BankAccountId");
                c.Property(x => x.FullName).IsRequired();
                c.Property(x => x.DateOfBirth).IsRequired().HasColumnType("datetimeoffset(7)");
                c.ToTable("CustomerVoForBankAccount", "Financial");
            });
        }
    }
}
