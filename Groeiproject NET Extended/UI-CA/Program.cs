using System.Threading.Tasks.Dataflow;
using Groeiproject.BL;
using Groeiproject.DAL;
using Groeiproject.DAL.EF;
using Groeiproject.UI.CA;
using Microsoft.EntityFrameworkCore;
using Repository = Groeiproject.DAL.EF.Repository;

DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<BakeryDbContext>();
optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\GroeiprojectDatabase.db");

BakeryDbContext context = new BakeryDbContext(optionsBuilder.Options);
//InMemoryStorage storage = new InMemoryStorage();
IRepository repository = new Repository(context);
IManager manager = new Manager(repository);

if(context.CreateDatabase(true)) DataSeeder.Seed(context);

ConsoleUI consoleUi = new ConsoleUI(manager);
consoleUi.Run();