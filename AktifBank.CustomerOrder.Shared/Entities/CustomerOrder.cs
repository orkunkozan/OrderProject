using AktifBank.CustomerOrder.Shared.Entities.Base;

namespace AktifBank.CustomerOrder.Shared.Entities
{
    public class CustomerOrder : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Address { get; set; } = string.Empty;


        public Customer Customer { get; set; }  
        public List<CustomerOrderDetail> CustomerOrderDetails { get; set; }  
    }
}
