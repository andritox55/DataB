using DataB.Data;
using DataB.Models;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;

namespace DataB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Hämtar alla kunder med specifik info

            using (var context = new NorthContext())
            {
                Console.WriteLine("Skriv in F för fallande och S för stigande");
                string Sorted = Console.ReadLine();

                bool ascending = true;

                //Kollar användaren val, om f för fallande och s för stigande

                if (Sorted.ToLower() == "f")
                {
                    ascending = false;
                }

                //sparar data från databasen i customersq i tabellen customers
                var customersq = from customer in context.Customers
                                select new
                                {
                                    customer.CompanyName,
                                    customer.Country,
                                    customer.Region,
                                    customer.Phone,
                                    orders = customer.Orders.Count(),
                                };


                //om ascending är true kommer företags namn sorteras stigande annars fallande
                if (ascending )
                {
                    customersq = customersq.OrderBy(o => o.CompanyName);
                }
                else
                {
                    customersq = customersq.OrderByDescending(o => o.CompanyName);
                }

                var customers = customersq.ToList();

                //forlopar listan med data från sparade listan från tabellen Customers
                foreach ( var c in customers )
                {
                    Console.WriteLine($"Företagsnamn: {c.CompanyName}");
                    Console.WriteLine($"Land: {c.Country}");
                    Console.WriteLine($"Region: {c.Region}");
                    Console.WriteLine($"Telefonnummer {c.Phone}");
                    Console.WriteLine($"Antal Order: {c.orders}");
                    Console.WriteLine();
                }

            }

            // Användaren väljer kund baserad på ID och informationen hämtas utifrån ID
            using (var context = new NorthContext())
            {
                Console.WriteLine("Välj en kund från listan");

                //sparar alla kunder och inkluderar deras ordrar
                var Customers = context.Customers.Include(c => c.Orders).ToList();

                //kollar genom tabellen med alla kunder och visar deras ID
                foreach ( var customer in Customers )
                {
                    Console.WriteLine($"Kund ID: {customer.CustomerId}");
                }

                Console.WriteLine();

                //här får användaren välja kund ID
                Console.Write("Välj: ");
                var choice = Console.ReadLine();

                //informationen med kundens ID sparas
                var CustomerChoice = Customers.FirstOrDefault(o => o.CustomerId == choice);

                //Så länge användaren skriver in eller ID stämmer med ID i tabellen visas kundinfo
                if ( CustomerChoice != null )
                {
                    Console.WriteLine($"Företagsnamn: {CustomerChoice.CompanyName}");
                    Console.WriteLine($"Land: {CustomerChoice.Country}");
                    Console.WriteLine($"Region: {CustomerChoice.Region}");
                    Console.WriteLine($"Telefonnummer: {CustomerChoice.Phone}");
                    Console.WriteLine($"Fax: {CustomerChoice.Fax}");
                    Console.WriteLine($"Adress: {CustomerChoice.Address}");
                    Console.WriteLine($"Postnummer: {CustomerChoice.PostalCode}");

                    //lopar genom tabellen ordrarna som är kopplade med kunden och visar deras ID
                    foreach (var orders in CustomerChoice.Orders)
                    {
                        Console.WriteLine($"Ordrar: {orders.OrderId}");
                    }
                }
                else
                {
                    Console.WriteLine("Kunden hittades inte");
                }
            }

            Console.WriteLine();

            //Användaren kan lägga in en ny kund i tabellen Customers
            using (var context = new NorthContext())
            {

                //ny objekt för att kunna lägga in kunder
                var NewCustomer = new Customer();

          
                Random random = new Random();

                //alla bokstäver användaren kan välja
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                char[] RandomChars = new char[5];

                //lopar genom längden på arrayen och lägger till en random bostav i varje index
                for (int i = 0; i < RandomChars.Length; i++)
                {
                    RandomChars[i] = chars[random.Next(chars.Length)];
                }

                //gör arrayen till en string med bokstäver
                string CId = new string(RandomChars);

                //lägger till ID för användaren manuellt genom random
                NewCustomer.CustomerId = CId;


                //här får användaren välja info som nya användaren ska ha
                Console.Write("Välg Företagsnamn: ");
                NewCustomer.CompanyName = Console.ReadLine();

                Console.Write("Välj Land: ");
                NewCustomer.Country = Console.ReadLine();

                Console.Write("Välj Region: ");
                NewCustomer.Region = Console.ReadLine();

                Console.Write("Välj Telefonnummer: ");
                NewCustomer.Phone = Console.ReadLine();

                Console.Write("välj Fax: ");
                NewCustomer.Fax = Console.ReadLine();

                Console.Write("Väl adress: ");
                NewCustomer.Address = Console.ReadLine();

                Console.Write("Välj Postnummer: ");
                NewCustomer.PostalCode = Console.ReadLine();

                //lägger till kunden i listan och sparar ändringarna
                context.Customers.Add(NewCustomer);
                context.SaveChanges();

                Console.WriteLine();
                Console.WriteLine("Kunden las till");
            }

        }
    }
}
