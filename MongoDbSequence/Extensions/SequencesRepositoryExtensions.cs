using System.Globalization;
using MongoDbSequence.Repositories;

namespace MongoDbSequence.Extensions;

public static class SequencesRepositoryExtensions
{
    public static string INVOICES_KEY => "invoices";
    
    public static string USERS_KEY => "users";

    public static async Task<string> InvoiceNumberAsync(this SequencesRepository repository)
    {
        var i = await repository
            .FetchAndAddAsync(INVOICES_KEY, CancellationToken.None)
            .ConfigureAwait(false);
        var year = DateTime.UtcNow.Year;
        var number = i.ToString(CultureInfo.InvariantCulture).PadLeft(6, '0');

        return $"{number}/{year}";
    }
    
    public static async Task<string> UserNumberAsync(this SequencesRepository repository, string department)
    {
        var i = await repository.FetchAndAddAsync(USERS_KEY, CancellationToken.None).ConfigureAwait(false);
        var number = i.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0');

        return $"{department.ToUpperInvariant()}{number}";
    }
}