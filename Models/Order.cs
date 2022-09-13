namespace CasusIJK.Models
{
    public class Order
    {
        public int Id { get; init; }
        public int KlantId { get; set; }
        public DateTime DatumGeorderd { get; init; }
        public string? OrderStatus { get; set; }    //voor nu even of "afgehandeld" of null, later enum of tabel?
    }
}