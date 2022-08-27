using AktifBank.CustomerOrder.Shared.Entities.Base;

namespace AktifBank.CustomerOrder.Shared.Entities
{
    public class Customer : BaseEntity
    {
        public string EMail { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<CustomerOrder> CustomerOrders { get; set; }  


    }
}
