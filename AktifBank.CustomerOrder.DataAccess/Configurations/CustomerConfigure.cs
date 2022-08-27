using AktifBank.CustomerOrder.DataAccess.Configurations.Base;
using AktifBank.CustomerOrder.Shared.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AktifBank.CustomerOrder.DataAccess.Configurations
{
    public class CustomerConfigure : BaseDefaultFieldConfigure<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
            builder.Property(x => x.EMail).HasMaxLength(160).IsRequired();

            builder.HasIndex(x => x.EMail).IsUnique(true);
        }
    }
}
