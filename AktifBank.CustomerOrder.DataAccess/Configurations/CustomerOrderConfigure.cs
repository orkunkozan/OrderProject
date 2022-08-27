using AktifBank.CustomerOrder.DataAccess.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AktifBank.CustomerOrder.DataAccess.Configurations
{
    public class CustomerOrderConfigure : BaseDefaultFieldConfigure<Shared.Entities.CustomerOrder>
    {
        public override void Configure(EntityTypeBuilder<Shared.Entities.CustomerOrder> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(255).IsRequired();
        }
    }
}
