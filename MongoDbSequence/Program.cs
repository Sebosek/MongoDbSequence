using System.Reflection;
using MongoDB.Driver;
using MongoDBMigrations;
using MongoDbSequence.Configurations;
using MongoDbSequence.Extensions;
using MongoDbSequence.Repositories;

SeedDatabase();

var config = new MongoDbConfiguration();
var client = new MongoClient(config.ConnectionString);
var repository = new SequencesRepository(config, client);

var john = await repository.UserNumberAsync("IT");
var mary = await repository.UserNumberAsync("ACC");

Console.WriteLine($"User {mary} paid for invoice {await repository.InvoiceNumberAsync()}");
Console.WriteLine($"User {john} created an invoice {await repository.InvoiceNumberAsync()}");
Console.ReadLine();

static void SeedDatabase()
{
    var config = new MongoDbConfiguration();
    var cts = new CancellationTokenSource();
    var result = new MigrationEngine()
        .UseDatabase(config.ConnectionString, config.Database)
        .UseAssembly(Assembly.GetExecutingAssembly()).UseSchemeValidation(false)
        .UseCancelationToken(cts.Token)
        .Run(new(1, 0, 0));

    if (result.Success) return;
    
    Console.WriteLine("Database seed failed!");
    Environment.Exit(1);
}
