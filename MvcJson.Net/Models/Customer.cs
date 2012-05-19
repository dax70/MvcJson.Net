using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcJson.Net.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Preferred { get; set; }

        public DateTimeOffset LastPurchaseDate { get; set; }

        static Customer()
        {
            source = new Customer[]
            {
                new Customer{ Id = 1, Name = "Bike Retailer", Preferred = true, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(10.0))},
                new Customer{Id = 2, Name = "Coffee Shippers", Preferred = false, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(6.0))},
                new Customer{Id = 3, Name = "Movie Rentals", Preferred = true, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7.0))},
                new Customer{Id = 4, Name = "Stock Traders", Preferred = false, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(5.0))},
                new Customer{Id = 5, Name = "Travel Agency", Preferred = false, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(2.0))},          
                new Customer{Id = 6, Name = "Car Service", Preferred = true, LastPurchaseDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(1.0))}                    
            };
        }

        private static IEnumerable<Customer> source;

        public static IEnumerable<Customer> GetAll(Func<Customer, bool> filter = null)
        {
            return source.Where(filter != null ? filter : c => true);
        }
    }
}
