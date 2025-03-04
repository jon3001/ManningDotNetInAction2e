using ManningBooks;
using Microsoft.Data.Sqlite;
using System.Linq;

//using var keepAliveConnection = new SqliteConnection(CatalogContext.ConnectionString);
//keepAliveConnection.Open();

CatalogContext.SeedBooks();

string[] userRequests = [
    ".NET in Action",
    "Grokking Simplicity",
    "API Design Patterns",
    "EF Core in Action",
];

var tasks = new List<Task>();

foreach (var userRequest in userRequests)
{
    tasks.Add(CatalogContext.WriteBookToConsoleAsync(userRequest));
}

Task.WaitAll(tasks.ToArray());

