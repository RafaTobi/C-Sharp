using Groeiproject.BL.Domain;
using Groeiproject.DAL.EF;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.DAL;

public class Repository : IRepository
{
    private BakeryDbContext _storage;

    public Repository(BakeryDbContext storage)
    {
        _storage = storage;
    }

    public Baker ReadBaker(int id)
    {
        foreach (Baker baker in InMemoryStorage._bakers)
        {
            if (baker.Id == id)
            {
                return baker;
            }
        }

        return null;
    }

    public IEnumerable<Baker> ReadAllBakers()
    {
        return InMemoryStorage._bakers;
    }

    public IEnumerable<Baker> ReadBakersByBakery(Bakery bakery)
    {
        List<Baker> bakersByBakery = new List<Baker>();
        foreach (Contract contract in bakery.Contracts)
        {
            bakersByBakery.Add(contract.Baker);
        }

        return bakersByBakery;
    }

    public void CreateBaker(Baker baker)
    {
        InMemoryStorage._bakers.Add(baker);
    }

    public IEnumerable<Baker> ReadAllBakersWithBakeries()
    {
        throw new NotImplementedException();
    }

    public Bakery ReadBakery(int id)
    {
        foreach (Bakery bakery in InMemoryStorage._bakeries)
        {
            if (bakery.Id == id)
            {
                return bakery;
            }
        }

        return null;
    }

    public IEnumerable<Bakery> ReadAllBakeries()
    {
        return InMemoryStorage._bakeries;
    }

    public IEnumerable<Baker> ReadBakersByNameAndBirth(string partOfName, DateTime birthDate)
    {
        // DateTime chosenBirth;
        // if (!string.IsNullOrWhiteSpace(birthDate))
        // {
        //     chosenBirth = DateTime.Parse(birthDate);
        // }
        // else chosenBirth = new DateTime(1, 1, 1);

        List<Baker> resultList = InMemoryStorage._bakers;
        if (partOfName != null)
        {
            resultList = resultList.FindAll(baker => baker.Name.Contains(partOfName));
        }

        if (birthDate != new DateTime(1, 1, 1))
        {
            resultList = resultList.FindAll(baker => baker.DateOfBirth.Equals(birthDate));
        }

        return resultList;
    }

    public void CreateBakery(Bakery bakery)
    {
        InMemoryStorage._bakeries.Add(bakery);
    }

    public IEnumerable<Bakery> ReadAllBakeriesWithBakersAndBreads()
    {
        throw new NotImplementedException();
    }

    public void CreateContract(Contract contract)
    {
        throw new NotImplementedException();
    }

    public void DeleteContract(int bakerId, int bakeryId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bakery> ReadBakeriesOfBaker(int bakerId)
    {
        throw new NotImplementedException();
    }

    public Bakery ReadBakeryWithBakers(int bakeryId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bread> ReadAllBreads()
    {
        throw new NotImplementedException();
    }

    public Bread ReadBread(int id)
    {
        throw new NotImplementedException();
    }

    public void CreateBread(Bread bread)
    {
        throw new NotImplementedException();
    }

    public bool BreadExists(int breadId)
    {
        throw new NotImplementedException();
    }

    public Baker ReadBakerWithBakeries(int id)
    {
        throw new NotImplementedException();
    }

    public bool ContractExists(int contractId)
    {
        throw new NotImplementedException();
    }

    public void CreateBakeryWithMaintainer(Bakery bakery)
    {
        throw new NotImplementedException();
    }

    public void UpdateAddress(int id, string newAddress)
    {
        throw new NotImplementedException();
    }
}