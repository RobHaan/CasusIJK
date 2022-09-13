using Microsoft.EntityFrameworkCore;
using CasusIJK.Models;

namespace CasusIJK.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<KlantInformatie> KlantInformaties { get; set; }
        public DbSet<Product> Producten { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRegel> OrderRegels { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) 
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderRegel>().HasKey(orderRegel => new { orderRegel.OrderId, orderRegel.ProductId });

            //seed dummy data
            //producten
            modelBuilder.Entity<Product>().HasData(
                new Product{Id = 1 ,ProductNaam = "Broodje Kaas", ProductOmschrijving = "Een lekker broodje met oude kaas.", ProductPrijs = 2.10f });
            modelBuilder.Entity<Product>().HasData(
                new Product{Id = 2,ProductNaam = "Broodje Gezond", ProductOmschrijving = "Een lekker broodje gezond.", ProductPrijs = 3.50f });
            modelBuilder.Entity<Product>().HasData(
                new Product{Id = 3,ProductNaam = "Steenoven Pizza", ProductOmschrijving = "Een pizza vers uit onze eigen oven, Rijkelijk belegd met het ingredient van de dag.", ProductPrijs = 8.00f });

            //klanten
            modelBuilder.Entity<Klant>().HasData(
                new Klant{Id = 1});
            modelBuilder.Entity<Klant>().HasData(
                new Klant{Id = 2});
            modelBuilder.Entity<Klant>().HasData(
                new Klant{Id = 3});

            //klantInformatie
            modelBuilder.Entity<KlantInformatie>().HasData(
                new KlantInformatie{KlantId = 1, KlantNaam = "Sjaak", Adres = "ChocoladeStraat 5"});
            modelBuilder.Entity<KlantInformatie>().HasData(
                new KlantInformatie{KlantId = 2, KlantNaam = "Nummer twee", Adres = "Dosweg 3"});
            modelBuilder.Entity<KlantInformatie>().HasData(
                new KlantInformatie{KlantId = 3, KlantNaam = "Ross", Adres = "Bosweg 1"});

            //Orders
            modelBuilder.Entity<Order>().HasData(
                new Order{Id = 1, KlantId = 1, DatumGeorderd = DateTime.Now, OrderStatus = "Afgehandeld"});
            modelBuilder.Entity<Order>().HasData(
                new Order{Id = 2, KlantId = 1, DatumGeorderd = DateTime.Now, OrderStatus = "Afgehandeld"});
            modelBuilder.Entity<Order>().HasData(
                new Order{Id = 3, KlantId = 1, DatumGeorderd = DateTime.Now, OrderStatus = null});
            modelBuilder.Entity<Order>().HasData(
                new Order{Id = 4, KlantId = 3, DatumGeorderd = DateTime.Now, OrderStatus = "Afgehandeld"});
            modelBuilder.Entity<Order>().HasData(
                new Order{Id = 5, KlantId = 3, DatumGeorderd = DateTime.Now, OrderStatus = null});

            //OrderRegels
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 1, ProductId = 1, Aantal = 3});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 1, ProductId = 3, Aantal = 1});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 2, ProductId = 2, Aantal = 2});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 3, ProductId = 3, Aantal = 3});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 4, ProductId = 1, Aantal = 1});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 4, ProductId = 2, Aantal = 1});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 4, ProductId = 3, Aantal = 1});
            modelBuilder.Entity<OrderRegel>().HasData(
                new OrderRegel{OrderId = 5, ProductId = 1, Aantal = 55});

            


            //Console.WriteLine(" -!- -!- -!- Deze code word uitgevoerd -!- -!- -!-");
        }
    }
}