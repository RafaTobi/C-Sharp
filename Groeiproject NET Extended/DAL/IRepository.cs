using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.DAL;

public interface IRepository
{
    Baker ReadBaker(int id);
    IEnumerable<Baker> ReadAllBakers();
    IEnumerable<Baker> ReadBakersByBakery(Bakery bakery);
    void CreateBaker(Baker baker);
    IEnumerable<Baker> ReadAllBakersWithBakeries();
    
    Bakery ReadBakery(int id);
    IEnumerable<Bakery> ReadAllBakeries();
    IEnumerable<Baker> ReadBakersByNameAndBirth(string? name, DateTime birthDate);
    void CreateBakery(Bakery bakery);
    IEnumerable<Bakery> ReadAllBakeriesWithBakersAndBreads();

    void CreateContract(Contract contract);
    void DeleteContract(int bakerId, int bakeryId);
    IEnumerable<Bakery> ReadBakeriesOfBaker(int bakerId);
    Bakery ReadBakeryWithBakers(int bakeryId);
    IEnumerable<Bread> ReadAllBreads();
    Bread ReadBread(int id);
    void CreateBread(Bread bread);
    bool BreadExists(int breadId);
    Baker ReadBakerWithBakeries(int id);
    bool ContractExists(int contractId);
    
    void CreateBakeryWithMaintainer(Bakery bakery);
    void UpdateAddress(int id, string newAddress);
}