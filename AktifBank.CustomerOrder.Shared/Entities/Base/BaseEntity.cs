using AktifBank.CustomerOrder.Shared.Entities.Base.Abstract;

namespace AktifBank.CustomerOrder.Shared.Entities.Base
{
    public class BaseEntity : IHasDefaultFields
    {
        public int Id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
