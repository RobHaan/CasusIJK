using Microsoft.AspNetCore.Mvc;
using CasusIJK.Models;
using CasusIJK.Data;

namespace CasusIJK.Controllers
{
    [Route("api/Klant")]
    [ApiController]
    public class KlantController : ControllerBase
    {
        private readonly ApiContext _context;

        public KlantController(ApiContext context)
        {
            _context = context;
        }

        //toevoegen en wijzigen van een klant
        [HttpPost]
        public JsonResult KlantPost(KlantInformatie klantInformatie)
        {
            //Kijk of de Klant al bestaat in de database
            var klantInformatieDb = _context.KlantInformaties.Find(klantInformatie.KlantId);

            //Als de klant niet bestaat, nieuwe klant toevoegen en anders wijzigen
            if (klantInformatieDb == null){

                //Nieuwe Klant toevoegen
                Klant nieuweKlant = new Klant();
                nieuweKlant.Id = klantInformatie.KlantId;
                _context.Klanten.Add(nieuweKlant);
                _context.KlantInformaties.Add(klantInformatie);
                _context.SaveChanges();

                return new JsonResult(Ok(klantInformatie));
            } else{

                //Klant bestaat al
                //Valideer of Klantinformatie gewijzigd mag worden?
                    //bijv: heeft de klant openstaande orders?

                //Wijzig data van klant
                _context.Entry(klantInformatieDb).CurrentValues.SetValues(klantInformatie);
                _context.SaveChanges();

                return new JsonResult(Ok(klantInformatie));
            }
        }

        //Verwijderen van een klant (Er wordt alleen de klantinformatie verwijderd, de KlantId waar de orders aan gekoppeld zijn blijft bestaan)
        [HttpDelete("{id}")]
        public JsonResult KlantDelete(int id)
        {
            KlantInformatie? klantTeDeleten = _context.KlantInformaties.Find(id);
        
            if (klantTeDeleten == null)
            {
                return new JsonResult(NotFound(id));
            }

            //valideer of klantinformatie verwijderd mag worden op dit moment
                //geen lopende transacties etc.

            _context.KlantInformaties.Remove(klantTeDeleten);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }


        //Een lijst van alle klanten met de orders per klant.
        [HttpGet]
        public JsonResult GetAllKlantenMetOrders()
        {
            List<Klant> gevondenKlanten = _context.Klanten.ToList();

            if (gevondenKlanten.Count == 0){
                return new JsonResult(NoContent());
            }

            List<KlantMetOrders> gevondenResultaten = new List<KlantMetOrders>();

            foreach (Klant klant in gevondenKlanten)
            {
                IEnumerable<Order> gevondenOrders = vindOrdersBijKlant(klant.Id);
                gevondenResultaten.Add( new KlantMetOrders{klant = klant, orders = gevondenOrders} ); 
            }

            return new JsonResult(Ok(gevondenResultaten));

    
        }

        //Gegevens van de klant met de orders
        [HttpGet("{id}")]
        public JsonResult GetKlantInformatieEnOrders(int id)
        {
            KlantInformatie? gevondenKlant = _context.KlantInformaties.Find(id);

            if (gevondenKlant == null){
                return new JsonResult(NotFound(id));
            } else{

                IEnumerable<Order> gevondenOrders = vindOrdersBijKlant(id);

                KlantInformatieMetOrders gevondenKlantInformatieMetOrders = new KlantInformatieMetOrders{ klantInformatie = gevondenKlant, orders = gevondenOrders};

                return new JsonResult(Ok(gevondenKlantInformatieMetOrders));
            }
        }

        private IEnumerable<Order> vindOrdersBijKlant(int id)
        {
            IEnumerable<Order> gevondenOrders = _context.Orders.Where(order => order.KlantId == id);
            return gevondenOrders;
        }

    }
}