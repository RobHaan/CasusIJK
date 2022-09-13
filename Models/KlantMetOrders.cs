namespace CasusIJK.Models
{
    public class KlantMetOrders
    {
        public Klant? klant { get; set;}

        public IEnumerable<Order>?   orders {get; set;}
    }
}