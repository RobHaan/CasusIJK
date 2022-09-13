namespace CasusIJK.Models
{
    public class OrderRegel
    {
        //orderId en ProductId zijn samen een composite primary key
        public int OrderId { get; init; }
        public int ProductId { get; init; }
        public int Aantal { get; set; }
    }
}