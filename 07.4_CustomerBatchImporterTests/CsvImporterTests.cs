using CustomerBatchImporter;
using FakeItEasy;
using System.Text;
using Xunit;

namespace CustomerBatchImporter.UnitTests;

public class CsvImporterTests
{
    private readonly ICustomerRepository _fakeCustomerRepo;
    private readonly CsvImporter _csvImporter;

    public CsvImporterTests()
    {
        _fakeCustomerRepo = A.Fake<ICustomerRepository>();
        _csvImporter = new(_fakeCustomerRepo);
    }

    private Stream GetStreamFromString(string content) => new MemoryStream(Encoding.UTF8.GetBytes(content));

    [Fact]
    public async Task ValidCustomerOneLine()
    {
        // Arrange
        string email = "some@email.com";
        string name = "A Customer";
        string license = "Basic";
        string csv = string.Join(',', email, name, license);
        A.CallTo(() => _fakeCustomerRepo
            .GetByEmailAsync(email))
            .Returns(default(Customer));

        // Act
        var stream = GetStreamFromString(csv);
        await _csvImporter.ReadAsync(stream);

        // Assert
        A.CallTo(() =>
          _fakeCustomerRepo.GetByEmailAsync(email))
          .MustHaveHappened();

        A.CallTo(() => _fakeCustomerRepo.CreateAsync(
            A<NewCustomerDto>.That.Matches(n =>
              n.Email == email && 
              n.Name == name && 
              n.License == license)))
          .MustHaveHappened();
    }

}
