using Microsoft.AspNetCore.Mvc;
using CasusIJK.Models;
using CasusIJK.Data;

namespace CasusIJK.Controllers
{
    [Route("api/Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApiContext _context;

        public OrderController(ApiContext context)
        {
            _context = context;
        }

        //toevoegen en wijzigen van een order
        [HttpPost]
        public JsonResult PostOrder(Order order)
        {

            //valideer of verwijzing naar klantId bestaat anders return error
            Klant? gevondenKlant = _context.Klanten.Find(order.KlantId);
            if (gevondenKlant == null){
                return new JsonResult(NotFound("KlantId niet gevonden"));
            }

            //Kijk of de order al bestaat in de database
            var orderInDb = _context.Orders.Find(order.Id);

            //Als de order niet bestaat, nieuwe order toevoegen en anders wijzigen
            if (orderInDb == null){

                //Nieuwe order toevoegen
                Order nieuweOrder = new Order {
                    Id = order.Id,
                    KlantId = order.KlantId,
                    DatumGeorderd = DateTime.Now,
                    OrderStatus = order.OrderStatus
                    };

                _context.Orders.Add(nieuweOrder);
                _context.SaveChanges();

                return new JsonResult(Ok(nieuweOrder));
            } else{
                //Order bestaat al
                
                //Wijzig data van order
                _context.Entry(orderInDb).CurrentValues.SetValues(order);
                _context.SaveChanges();

                return new JsonResult(Ok(order));
            }
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteOrder(int id)
        {
            Order? orderTeDeleten = _context.Orders.Find(id);
        
            if (orderTeDeleten == null)
            {
                return new JsonResult(NotFound(id));
            }

            //valideer of order verwijderd mag worden op dit moment
                //wat is de status etc

            _context.Orders.Remove(orderTeDeleten);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }


    }
}