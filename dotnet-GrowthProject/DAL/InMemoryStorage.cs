using Groeiproject.BL.Domain;

namespace Groeiproject.DAL;

public class InMemoryStorage
{
    public static List<Baker> _bakers { get; set; }
    public static List<Bakery> _bakeries { get; set; }

    public static void Seed()
    {
        _bakers = new List<Baker>();
        _bakeries = new List<Bakery>();

        Baker baker1 = new Baker() { Id = 1, Name = "Thomas", DateOfBirth = new DateTime(1975, 6, 24) };
        Baker baker2 = new Baker() { Id = 2, Name = "Jens", DateOfBirth = new DateTime(1987, 12, 28) };
        Baker baker3 = new Baker() { Id = 3, Name = "Amelie", DateOfBirth = new DateTime(1993, 3, 8) };
        Baker baker4 = new Baker() { Id = 4, Name = "Wout", DateOfBirth = new DateTime(1999, 1, 15) };
        
        Bakery bakery1 = new Bakery() { Id = 1, Adres = "123 Oak Avenue", Name = "Gold's Baker" };
        Bakery bakery2 = new Bakery() { Id = 2, Adres = "456 Side Drive", Name = "Granny's Way" };
        Bakery bakery3 = new Bakery() { Id = 3, Adres = "789 Main Street", Name = "Baker Anita" };
        Bakery bakery4 = new Bakery() { Id = 4, Adres = "201 Cherry Lane", Name = "The warm bakery" };

        /*
        baker1.Bakeries.Add(bakery1);
        baker1.Bakeries.Add(bakery2);
        baker2.Bakeries.Add(bakery1);
        baker3.Bakeries.Add(bakery3);
        baker4.Bakeries.Add(bakery1);
        baker4.Bakeries.Add(bakery4);

        bakery1.Bakers.Add(baker1);
        bakery2.Bakers.Add(baker3);
        bakery3.Bakers.Add(baker3);
        bakery3.Bakers.Add(baker4);*/

        _bakeries.Add(bakery1);
        _bakeries.Add(bakery2);
        _bakeries.Add(bakery3);
        _bakeries.Add(bakery4);
        _bakers.Add(baker1);
        _bakers.Add(baker2);
        _bakers.Add(baker3);
        _bakers.Add(baker4);
    }
}