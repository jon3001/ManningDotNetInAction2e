namespace CustomerBatchImporter;

public class CsvImporter
{
    private readonly ICustomerRepository _customerRepo;

    public CsvImporter(ICustomerRepository customerRepo) => _customerRepo = customerRepo;

    public async Task ReadAsync(Stream stream)
    {
        var reader = new StreamReader(stream);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var customer = ReadCsvLine(line);
            if (customer == null)
            {
                continue;
            }

            var existing = await _customerRepo.GetByEmailAsync(customer.Email);

            if (existing == null)
            {
                await _customerRepo.CreateAsync(customer);
            }
            else
            {
                await _customerRepo.UpdateAsync(
                  new UpdateCustomerDto(
                    existing.Id,
                    customer.Name,
                    customer.License
                  ));
            }
        }
    }

    /// <summary>
    /// This is a crude implementation. CSV data with an embedded comma is generally further escaped with double quotes.
    /// Consider abstracting the CSB parsing behind an interface and using a Nuget library such as:
    /// * GenericParser
    /// * NReco.Csv
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private NewCustomerDto? ReadCsvLine(string line)
    {
        var el = line.Split(',');
        return el.Length != 3 
            ? null
            : new NewCustomerDto(el[0], el[1], el[2]);
    }
}
