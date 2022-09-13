namespace CasusIJK.Models
{
    public class KlantInformatieMetOrders
    {
        public KlantInformatie? klantInformatie { get; set;}

        public IEnumerable<Order>?   orders {get; set;}
    }
}