namespace AktifBank.CustomerOrder.Shared.Entities.Base.Abstract
{
    public interface IHasDefaultFields
    {
        public int Id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
