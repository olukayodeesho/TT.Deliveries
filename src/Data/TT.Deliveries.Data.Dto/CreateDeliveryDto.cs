namespace TT.Deliveries.Data.Dto
{
    //public class CreateDeliveryDto
    //{
    //    public DeliveryState State { get; set; }
    //}

    public class CreateDeliveryDto
    {
        public DeliveryState State { get; set; }
        public AccessWindowDto AccessWindow { get; set; }
        public RecipientDto Recipient { get; set; }
        public OrderDto Order { get; set; }
    }
}
