using System.ComponentModel.DataAnnotations;
using Groeiproject.BL;
using Groeiproject.BL.Domain;
using Groeiproject.UI.CA.Extensions;

namespace Groeiproject.UI.CA;

public class ConsoleUI
{
    private IManager _manager;

    public ConsoleUI(IManager manager)
    {
        _manager = manager;
    }

    public void Run()
    {
        int userSelection = 0;
        do
        {
            Console.WriteLine("What would you like to do?\n==========================");
            Console.WriteLine("0) Quit\n" +
                              "1) Show all bakeries\n" +
                              "2) Show all bakers working for specific bakeries\n" +
                              "3) Show all bakers\n" +
                              "4) Show all bakers by name/date of birth\n" +
                              "5) Add new Baker\n" +
                              "6) Add new Bakery\n" +
                              "7) Add baker to bakery\n" +
                              "8) Remove baker from bakery");
            Int32.TryParse(Console.ReadLine(), out userSelection);
            switch (userSelection)
            {
                case 1:
                    CaseOneShowAllBakeries();
                    break;
                case 2:
                    CaseTwoShowBakersForBakery();
                    break;
                case 3:
                    CaseThreeShowAllBakers();
                    break;
                case 4:
                    CaseFourShowBakersByNameAndBirth();
                    break;
                case 5:
                    CaseFiveAddBaker();
                    break;
                case 6:
                    CaseSixAddBakery();
                    break;
                case 7:
                    CaseSevenAddBakerToBakery();
                    break;
                case 8:
                    CaseEightRemoveBakerFromBakery();
                    break;
            }
        } while (userSelection != 0);
    }

    public void CaseOneShowAllBakeries()
    {
        Console.WriteLine("All Bakeries\n============");
        foreach (Bakery bakery in _manager.GetAllBakeriesWithBakersAndBreads())
        {
            Console.WriteLine(bakery.GetInfo());
        }

        Console.WriteLine();
    }

    public void CaseTwoShowBakersForBakery()
    {
        int subSelection;
        Console.Write("What bakery? (1=Gold's Baker, 2=Granny's Way, 3=Baker Anita, 4=The warm bakery)");
        Int32.TryParse(Console.ReadLine(), out subSelection);
        List<Bakery> bakeries = _manager.GetAllBakeriesWithBakersAndBreads().ToList();
        foreach (Contract contract in bakeries[subSelection - 1].Contracts)
        {
            Console.WriteLine(contract.Baker.Name);
        }

        Console.WriteLine();
    }

    public void CaseThreeShowAllBakers()
    {
        Console.WriteLine("All Bakers\n==========");
        foreach (Baker baker in _manager.GetAllBakersWithBakeries())
        {
            Console.WriteLine(baker.GetInfo());
        }

        Console.WriteLine();
    }

    public void CaseFourShowBakersByNameAndBirth()
    {
        Console.Write("Enter (part of) a name or leave blank: ");
        string partOfName = Console.ReadLine();
        Console.Write("Enter a full date (yyyy/mm/dd) or leave blank: ");
        string birthString = Console.ReadLine();

        List<Baker> result;
        result = _manager.GetBakersByNameAndBirth(partOfName, birthString).ToList();

        foreach (Baker filteredBaker in result)
        {
            Console.WriteLine(filteredBaker.GetInfo());
        }

        Console.WriteLine();
    }

    public void CaseFiveAddBaker()
    {
        bool addSuccessful;
        do
        {
            Console.WriteLine("\n====Adding new baker====");
            Console.Write("New name for baker: ");
            string bakerName = Console.ReadLine();
            bool validDate;
            DateTime bakerBirthDate;
            do
            {
                Console.Write("Baker's date of birth: ");
                string dateString = Console.ReadLine();
                validDate = DateTime.TryParse(dateString, out bakerBirthDate);
            } while (!validDate);

            try
            {
                _manager.AddBaker(bakerName, bakerBirthDate);
                addSuccessful = true;
            }
            catch (ValidationException exc)
            {
                foreach (string errorMsg in exc.Message.Split("|"))
                {
                    Console.WriteLine(errorMsg);
                }

                addSuccessful = false;
            }
        } while (!addSuccessful);

        Console.WriteLine();
    }

    public void CaseSixAddBakery()
    {
        bool addSuccessful;
        do
        {
            Console.WriteLine("\n====Adding new bakery====");
            Console.Write("New name for bakery: ");
            string bakeryName = Console.ReadLine();
            Console.Write("New adres of bakery: ");
            string bakeryAdres = Console.ReadLine();
            try
            {
                _manager.AddBakery(bakeryName, bakeryAdres);
                addSuccessful = true;
            }
            catch (ValidationException exc)
            {
                foreach (string errorMsg in exc.Message.Split("|"))
                {
                    Console.WriteLine(errorMsg);
                }

                addSuccessful = false;
            }
        } while (!addSuccessful);

        Console.WriteLine();
    }

    public void CaseSevenAddBakerToBakery()
    {
        Console.WriteLine("Which bakery would you like to add a baker to?");
        List<Bakery> bakeries = _manager.GetAllBakeries().ToList();
        for (int i = 1; i <= bakeries.Count; i++)
        {
            Console.WriteLine(i + ") " + bakeries[i - 1].Name);
        }

        int userSelection;
        do
        {
            Console.Write("Your choice: ");
        } while (!Int32.TryParse(Console.ReadLine(), out userSelection) || userSelection <= 0);

        Bakery selectedBakery = bakeries[userSelection - 1];

        Console.WriteLine("Select a baker you want to add to this bakery:");
        List<Baker> bakers = _manager.GetAllBakers().ToList();
        for (int i = 1; i <= bakers.Count; i++)
        {
            Console.WriteLine(i + ") " + bakers[i - 1].Name);
        }

        do
        {
            Console.Write("Your choice: ");
        } while (!Int32.TryParse(Console.ReadLine(), out userSelection) || userSelection <= 0);

        Baker selectedBaker = bakers[userSelection - 1];

        Console.Write("The contract begins now..." +
                      "When does it end? (in months): ");
        int contractMonts;
        Int32.TryParse(Console.ReadLine(), out contractMonts);
        DateTime startDate = DateTime.Today;
        DateTime endDate = DateTime.Today.AddMonths(contractMonts);

        Console.Write("What is the worth of the contract: ");
        int price;
        Int32.TryParse(Console.ReadLine(), out price);

        Contract contract = new Contract()
            { Baker = selectedBaker, Bakery = selectedBakery, StartDate = startDate, EndDate = endDate, Price = price };
        _manager.AddContract(contract);
    }

    public void CaseEightRemoveBakerFromBakery()
    {
        Console.WriteLine("Which baker would you like to remove from a bakery?");
        List<Baker> bakers = _manager.GetAllBakers().ToList();
        for (int i = 1; i <= bakers.Count; i++)
        {
            Console.WriteLine(i + ") " + bakers[i - 1].Name);
        }

        int userSelection;

        do
        {
            Console.Write("Your choice: ");
        } while (!Int32.TryParse(Console.ReadLine(), out userSelection) || userSelection <= 0);

        int bakerId = bakers[userSelection - 1].Id;

        Console.WriteLine("Which bakery would you like to remove from this baker?");
        List<Bakery> bakeriesOfBaker = _manager.GetBakeriesOfBaker(userSelection).ToList();
        for (int i = 1; i <= bakeriesOfBaker.Count; i++)
        {
            Console.WriteLine(i + ") " + bakeriesOfBaker[i - 1].Name);
        }

        do
        {
            Console.Write("Your choice: ");
        } while (!Int32.TryParse(Console.ReadLine(), out userSelection) || userSelection <= 0);

        int bakeryId = bakeriesOfBaker[userSelection - 1].Id;

        _manager.RemoveContract(bakerId, bakeryId);
    }
}