using System.ComponentModel.DataAnnotations;

namespace CasusIJK.Models
{
    public class KlantInformatie
    {
        [Key]
        public int KlantId { get; set; }
        public string? KlantNaam { get; set; }
        public string? Adres { get; set; }
    }
}