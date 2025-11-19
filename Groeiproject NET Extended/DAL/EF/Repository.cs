using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Groeiproject.DAL.EF;

public class Repository : IRepository
{
    private readonly BakeryDbContext _context;

    public Repository(BakeryDbContext dbContext)
    {
        _context = dbContext;
    }

    public Baker ReadBaker(int id)
    {
        return _context.Bakers.FirstOrDefault(bakery => bakery.Id == id);
    }

    public IEnumerable<Baker> ReadAllBakers()
    {
        return _context.Bakers;
    }

    public IEnumerable<Baker> ReadBakersByBakery(Bakery bakery)
    {
        return _context.Bakers.Where(baker => baker.Contracts.Any(contract => contract.Bakery == bakery));
    }

    public void CreateBaker(Baker baker)
    {
        _context.Bakers.Add(baker);
        _context.SaveChanges();
    }

    public IEnumerable<Baker> ReadAllBakersWithBakeries()
    {
        return
            _context.Bakers.Include(b => b.Contracts)
                .ThenInclude(c => c.Bakery);
    }

    public Bakery ReadBakery(int id)
    {
        return _context.Bakeries.FirstOrDefault(bakery => bakery.Id == id);
    }

    public IEnumerable<Bakery> ReadAllBakeries()
    {
        return _context.Bakeries
                .Include(b => b.Maintainer);
    }

    public IEnumerable<Baker> ReadBakersByNameAndBirth(string partOfName, DateTime birthDate)
    {
        return _context.Bakers
            .Where(baker => (string.IsNullOrEmpty(partOfName) || baker.Name.Contains(partOfName))
                            && (birthDate == DateTime.MinValue || baker.DateOfBirth == birthDate))
            .ToList();
    }

    public void CreateBakery(Bakery bakery)
    {
        _context.Bakeries.Add(bakery);
        _context.SaveChanges();
    }

    public IEnumerable<Bakery> ReadAllBakeriesWithBakersAndBreads()
    {
        return
            _context.Bakeries.Include(b => b.Contracts)
                .ThenInclude(c => c.Baker)
                .Include(b => b.Breads);
    }

    public void CreateContract(Contract contract)
    {
       _context.Contracts.Add(contract);

       _context.SaveChanges();
    }

    public void DeleteContract(int bakerId, int bakeryId)
    {
        Contract contractToDelete = _context.Contracts
            .Single(c => c.Baker.Id == bakerId && c.Bakery.Id == bakeryId);

        _context.Remove(contractToDelete);
        _context.SaveChanges();
    }

    public IEnumerable<Bakery> ReadBakeriesOfBaker(int bakerId)
    {
        return _context.Bakeries.Where(bakery => bakery.Contracts.Any(contract => contract.Baker.Id == bakerId));
    }

    public Bakery ReadBakeryWithBakers(int bakeryId)
    {
        return _context.Bakeries
            .Include(b => b.Maintainer)
            .Include(b => b.Contracts)
            .ThenInclude(c => c.Baker)
            .SingleOrDefault(bakery => bakery.Id == bakeryId);
    }

    public IEnumerable<Bread> ReadAllBreads()
    {
        return _context.Breads;
    }

    public Bread ReadBread(int id)
    {
        return _context.Breads.SingleOrDefault(bread => bread.Id == id);
    }

    public void CreateBread(Bread bread)
    {
        _context.Breads.Add(bread);
        _context.SaveChanges();
    }

    public bool BreadExists(int breadId)
    {
        return _context.Breads.Any(bread => bread.Id == breadId);
    }

    public Baker ReadBakerWithBakeries(int id)
    {
        return _context.Bakers.Include(b => b.Contracts)
            .ThenInclude(c => c.Bakery)
            .SingleOrDefault(baker => baker.Id == id);    }

    public bool ContractExists(int contractId)
    {
        return _context.Contracts.Any(contract => contract.Id == contractId);
    }

    public void CreateBakeryWithMaintainer(Bakery bakery)
    {
        _context.Bakeries.Add(bakery);
        _context.SaveChanges();
    }

    public void UpdateAddress(int id, string newAddress)
    {
        Bakery bakeryToUpdate = _context.Bakeries.FirstOrDefault(b => b.Id == id);
        if (bakeryToUpdate != null)
        {
            bakeryToUpdate.Adres = newAddress;
        }

        _context.SaveChanges();
    }
}