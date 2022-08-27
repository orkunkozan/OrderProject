using AktifBank.CustomerOrder.Shared.Entities.Base.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AktifBank.CustomerOrder.DataAccess.Configurations.Base
{
    public abstract class BaseDefaultFieldConfigure<TEntity> where TEntity : class, IHasDefaultFields, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.AddTime).IsRequired();
            builder.Property(p => p.UpdateTime);

            builder.HasIndex(x => new { x.AddTime });
            builder.HasIndex(x => new { x.UpdateTime });
        }
    }
}
