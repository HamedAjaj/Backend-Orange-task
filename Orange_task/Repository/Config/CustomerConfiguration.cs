using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orange_task.Domain.Entities;

namespace Orange_task.Repository.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //builder.Property(c => c.id).IsRequired(errorMe);
            //builder.Property(c => c.CustomerName).HasMaxLength(50).IsRequired();
            //builder.Property(c => c.Service).HasMaxLength(50).IsRequired();
            //builder.Property(c => c.ContractDate).IsRequired();
            //builder.Property(c => c.ContractExpiryDate).IsRequired();
            
            
            builder.HasKey(e => e.id);

            builder.Property(e => e.CustomerName).IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Service).IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ContractDate).IsRequired();

            builder.Property(e => e.ContractExpiryDate).IsRequired();

         //   builder.HasCheckConstraint("CK_Customer_ContractDates", "ContractExpiryDate > ContractDate");
        }
    }
}
