using System.ComponentModel.DataAnnotations;
using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Groeiproject.DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public ManagerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    public void Dispose()
    {
    }

    [Fact]
    public void AddBakery_ShouldAddBakery_GivenValidAttributes()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var dbContext = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();

            //Arrange
            string name = "Added Bakery";
            string address = "123 Testroad";


            //Act
            mgr.AddBakery(name, address);

            var bakery = dbContext.Bakeries.SingleOrDefault(b => b.Name == name && b.Adres == address);

            //Assert
            Assert.NotNull(bakery);

            Assert.Equal(name, bakery.Name);
            Assert.Equal(address, bakery.Adres);
        }
    }

    [Fact]
    public void AddBakery_ShouldReturnErrorList_GivenInvalidAttributes()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            var dbContext = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();

            //Arrange
            string invalidName = ""; //Name is required
            string invalidAddress = "XX"; //Address moet minstens 4 characters lang zijn

            //Act
            var ex = Assert.Throws<ValidationException>(() => mgr.AddBakery(invalidName, invalidAddress));
            var bakery = dbContext.Bakeries.SingleOrDefault(b => b.Name == invalidName && b.Adres == invalidAddress);

            //Assert
            Assert.Contains("The Name field is required", ex.Message);
            Assert.Contains("The field Adres must be a string or array type with a minimum length of '4'.", ex.Message);
            Assert.Null(bakery);
        }
    }

    [Fact]
    public void UpdateAddress_ShouldChangeBakeriesAddress_GivenAValidNewAddress()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();

            //Arrange
            string name = "Bakery under Testing";
            string firstAddress = "1 First Address";

            var bakery = mgr.AddBakery(name, firstAddress);

            Assert.NotNull(bakery);
            int id = bakery.Id;

            //Act
            string secondAddress = "2";
            mgr.UpdateAddress(id, secondAddress);

            bakery = mgr.GetBakery(id);
            
            //Assert
            Assert.Equal(secondAddress, bakery.Adres); 
            Assert.Equal(name, bakery.Name);
            Assert.Equal(id, bakery.Id);
            Assert.NotEqual(firstAddress, bakery.Adres);
        }
    }

    [Fact]
    public void UpdateAddress_ShouldNotUpdateAddress_GivenInvalidAddress()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();

            //Arrange
            string name = "Bakery under Testing";
            string firstAddress = "1 First Address";

            var bakery = mgr.AddBakery(name, firstAddress);

            Assert.NotNull(bakery);
            int id = bakery.Id;

            //Act
            string secondAddress = "2";
            mgr.UpdateAddress(id, secondAddress);

            bakery = mgr.GetBakery(id);
            
            //Assert
            Assert.Equal(secondAddress, bakery.Adres); 
            Assert.Equal(name, bakery.Name);
            Assert.Equal(id, bakery.Id);
            Assert.NotEqual(firstAddress, bakery.Adres);
        }
    }
}