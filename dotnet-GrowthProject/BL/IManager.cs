using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.BL;

public interface IManager
{
    Baker GetBaker(int id);
    IEnumerable<Baker> GetAllBakers();
    IEnumerable<Baker> GetBakersByBakery(Bakery bakery);
    Baker AddBaker(string name, DateTime birthdate);
    IEnumerable<Baker> GetAllBakersWithBakeries();
    
    Bakery GetBakery(int id);
    IEnumerable<Bakery> GetAllBakeries();
    IEnumerable<Baker> GetBakersByNameAndBirth(string name, string birthDate);
    Bakery AddBakery(string name, string adres);
    IEnumerable<Bakery> GetAllBakeriesWithBakersAndBreads();
    
    void AddContract(Contract contract);
    void RemoveContract(int bakerId, int bakeryId);
    IEnumerable<Bakery> GetBakeriesOfBaker(int bakeryId);
    Bakery GetBakeryWithBakers(int id);
    
    IEnumerable<Bread> GetAllBreads();
    Bread GetBread(int id);
    void AddBread(Bread bread);
    bool BreadExists(int breadId);
    Baker GetBakerWithBakeries(int id);
    bool ContractExists(int contractId);

    Bakery AddBakeryWithMaintainer(string name, string address, IdentityUser maintainer);
    void UpdateAddress(int id, string newAddress);
}