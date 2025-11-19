using System.ComponentModel.DataAnnotations;
using System.Text;
using Groeiproject.BL.Domain;
using Groeiproject.DAL;
using Microsoft.AspNetCore.Identity;

namespace Groeiproject.BL;

public class Manager : IManager
{
    private IRepository _repo;

    public Manager(IRepository repo)
    {
        _repo = repo;
    }

    public Baker GetBaker(int id)
    {
        return _repo.ReadBaker(id);
    }

    public IEnumerable<Baker> GetAllBakers()
    {
        return _repo.ReadAllBakers();
    }

    public IEnumerable<Baker> GetBakersByBakery(Bakery bakery)
    {
        List<Baker> bakersByBakery = new List<Baker>();
        foreach (Contract contract in bakery.Contracts)
        {
            bakersByBakery.Add(contract.Baker);
        }

        return bakersByBakery;
    }

    public Baker AddBaker(string name, DateTime birthdate)
    {
        Baker bakerAdded = new Baker() { Name = name, DateOfBirth = birthdate };

        List<ValidationResult> errorList = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(bakerAdded, new ValidationContext(bakerAdded), errorList,
            validateAllProperties: true);

        if (!isValid)
        {
            StringBuilder strb = new StringBuilder();
            foreach (ValidationResult validationResult in errorList)
            {
                strb.Append("|" + validationResult.ErrorMessage);
            }

            throw new ValidationException(strb.ToString());
        }

        _repo.CreateBaker(bakerAdded);
        return bakerAdded;
    }

    public IEnumerable<Baker> GetAllBakersWithBakeries()
    {
        return _repo.ReadAllBakersWithBakeries();
    }

    public Bakery GetBakery(int id)
    {
        return _repo.ReadBakery(id);
    }

    public IEnumerable<Bakery> GetAllBakeries()
    {
        return _repo.ReadAllBakeries();
    }

    public IEnumerable<Baker> GetBakersByNameAndBirth(string partOfName, string birthDate)
    {
        DateTime chosenBirth;
        if (!string.IsNullOrWhiteSpace(birthDate))
        {
            if(DateTime.TryParse(birthDate, out chosenBirth));
        }
        else chosenBirth = new DateTime(1, 1, 1);
        
        return _repo.ReadBakersByNameAndBirth(partOfName, chosenBirth);
    }
    
    public Bakery AddBakery(string name, string adres)
    {
        Bakery bakeryAdded = new Bakery() { Name = name, Adres = adres };

        List<ValidationResult> errorList = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(bakeryAdded, new ValidationContext(bakeryAdded), errorList,
            validateAllProperties: true);

        if (!isValid)
        {
            StringBuilder strb = new StringBuilder();
            foreach (ValidationResult validationResult in errorList)
            {
                strb.Append("|" + validationResult.ErrorMessage);
            }

            throw new ValidationException(strb.ToString());
        }

        _repo.CreateBakery(bakeryAdded);
        return bakeryAdded;
    }

    public IEnumerable<Bakery> GetAllBakeriesWithBakersAndBreads()
    {
        return _repo.ReadAllBakeriesWithBakersAndBreads();
    }

    public void AddContract(Contract contract)
    {
        _repo.CreateContract(contract);
    }

    public void RemoveContract(int bakerId, int bakeryId)
    {
        _repo.DeleteContract(bakerId, bakeryId);
    }

    public IEnumerable<Bakery> GetBakeriesOfBaker(int bakeryId)
    {
        return _repo.ReadBakeriesOfBaker(bakeryId);
    }

    public Bakery GetBakeryWithBakers(int id)
    {
        return _repo.ReadBakeryWithBakers(id);
    }

    public IEnumerable<Bread> GetAllBreads()
    {
        return _repo.ReadAllBreads();
    }

    public Bread GetBread(int id)
    {
        return _repo.ReadBread(id);
    }

    public void AddBread(Bread bread)
    {
        _repo.CreateBread(bread);
    }

    public bool BreadExists(int breadId)
    {
        return _repo.BreadExists(breadId);
    }

    public Baker GetBakerWithBakeries(int id)
    {
        return _repo.ReadBakerWithBakeries(id);
    }

    public bool ContractExists(int contractId)
    {
        return _repo.ContractExists(contractId);
    }

    public Bakery AddBakeryWithMaintainer(string name, string address, IdentityUser maintainer)
    {
        Bakery bakery = new Bakery() { Name = name, Adres = address, Maintainer = maintainer };
        _repo.CreateBakeryWithMaintainer(bakery);
        return bakery;
    }

    public void UpdateAddress(int id, string newAddress)
    {
        _repo.UpdateAddress(id, newAddress);
    }
}