namespace CasusIJK.Models
{
    public class Product
    {
        public int Id { get; init; }
        public string? ProductNaam { get; set; }
        public string? ProductOmschrijving { get; set; }
        public float ProductPrijs { get; set; }
    }
}