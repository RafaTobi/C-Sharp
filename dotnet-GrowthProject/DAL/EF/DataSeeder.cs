using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.DAL.EF;

public static class DataSeeder
{
    public static void Seed(BakeryDbContext context)
    {
        Baker baker1 = new Baker() { Id = 1, Name = "Thomas", DateOfBirth = new DateTime(1975, 6, 24) };
        Baker baker2 = new Baker() { Id = 2, Name = "Jens", DateOfBirth = new DateTime(1987, 12, 28) };
        Baker baker3 = new Baker() { Id = 3, Name = "Amelie", DateOfBirth = new DateTime(1993, 3, 8) };
        Baker baker4 = new Baker() { Id = 4, Name = "Wout", DateOfBirth = new DateTime(1999, 1, 15) };
        
        Bakery bakery1 = new Bakery() { Id = 1, Adres = "123 Oak Avenue", Name = "Gold's Baker"};
        Bakery bakery2 = new Bakery() { Id = 2, Adres = "456 Side Drive", Name = "Granny's Way" };
        Bakery bakery3 = new Bakery() { Id = 3, Adres = "789 Main Street", Name = "Baker Anita" };
        Bakery bakery4 = new Bakery() { Id = 4, Adres = "201 Cherry Lane", Name = "The warm bakery" };

        Bread bread1 = new Bread() { Id = 10, Name = "Sandwich", Price = 0.2, Weight = 60, DateOfProduction = DateTime.Today.Date.AddDays(-1), Size = Size.Sandwich };
        Bread bread2 = new Bread() { Id = 20, Name = "Lang Wit", Price = 2.8, Weight = 600, DateOfProduction = DateTime.Today, Size = Size.Groot };
        Bread bread3 = new Bread() { Id = 30, Name = "Tijgerpistolet", Price = 0.4, Weight = 100, DateOfProduction = DateTime.Today.Date.AddDays(-1), Size = Size.Pistolet};
        Bread bread4 = new Bread() { Id = 40, Name = "Rond Grof", Price = 2.8, Weight = 450, DateOfProduction = DateTime.Today, Size = Size.Rond};

        Contract contract1 = new Contract { Baker = baker1, Bakery = bakery1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(12), Price = 1000};
        Contract contract2 = new Contract { Baker = baker2, Bakery = bakery2, StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(10), Price = 1200 };
        Contract contract3 = new Contract { Baker = baker3, Bakery = bakery3, StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(8), Price = 750 };
        Contract contract4 = new Contract { Baker = baker4, Bakery = bakery4, StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(6), Price = 600 };
        Contract contract5 = new Contract { Baker = baker1, Bakery = bakery4, StartDate = DateTime.Today, EndDate = DateTime.Today.AddMonths(4), Price = 400 };

        //Toevoegen maintainers aan bakeries
        bakery1.Maintainer = context.Users.Single(u => u.UserName == "user@app.com");
        bakery2.Maintainer = context.Users.Single(u => u.UserName == "user@app.com");
        bakery3.Maintainer = context.Users.Single(u => u.UserName == "second.user@app.com");
        bakery4.Maintainer = context.Users.Single(u => u.UserName == "rafael@app.com");
        
        baker1.Contracts.Add(contract1);
        baker2.Contracts.Add(contract2);
        baker3.Contracts.Add(contract3);
        baker4.Contracts.Add(contract4);
        baker1.Contracts.Add(contract5);
        
        bakery1.Contracts.Add(contract1);
        bakery2.Contracts.Add(contract2);
        bakery3.Contracts.Add(contract3);
        bakery4.Contracts.Add(contract4);
        bakery4.Contracts.Add(contract5);
        
        bakery1.Breads.Add(bread1);
        bakery1.Breads.Add(bread2);
        bakery2.Breads.Add(bread3);
        bakery3.Breads.Add(bread1);
        bakery3.Breads.Add(bread3);
        bakery3.Breads.Add(bread4);
        bakery4.Breads.Add(bread2);
        bakery4.Breads.Add(bread4);
        
        context.Contracts.Add(contract1);
        context.Contracts.Add(contract2);
        context.Contracts.Add(contract3);
        context.Contracts.Add(contract4);
        context.Contracts.Add(contract5);
        context.Bakeries.Add(bakery1);
        context.Bakeries.Add(bakery2);
        context.Bakeries.Add(bakery3);
        context.Bakeries.Add(bakery4);
        context.Bakers.Add(baker1);
        context.Bakers.Add(baker2);
        context.Bakers.Add(baker3);
        context.Bakers.Add(baker4);
        
        context.SaveChanges();
        context.ChangeTracker.Clear();
    }
}