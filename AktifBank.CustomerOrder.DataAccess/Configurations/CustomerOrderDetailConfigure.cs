using AktifBank.CustomerOrder.DataAccess.Configurations.Base;
using AktifBank.CustomerOrder.Shared.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AktifBank.CustomerOrder.DataAccess.Configurations
{
    public class CustomerOrderDetailConfigure : BaseDefaultFieldConfigure<CustomerOrderDetail>
    {
        public override void Configure(EntityTypeBuilder<CustomerOrderDetail> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CustomerOrderId).IsRequired();
            builder.Property(x => x.Barcode).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Explanation).HasMaxLength(255);
            builder.Property(x => x.Piece).IsRequired();
            builder.Property(x => x.Amount).IsRequired();

            builder.HasIndex(x => new { x.CustomerOrderId, x.Barcode }).IsUnique(true);
            builder.HasIndex(x => x.Barcode);
        }
    }
}
