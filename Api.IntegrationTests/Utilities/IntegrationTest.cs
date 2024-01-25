using AutoWarden.Database;
using Xunit;

namespace AutoWarden.Api.IntegrationTests.Utilities;

public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>, IDisposable
{
    protected readonly ApiWebApplicationFactory _factory;
    protected readonly HttpClient _client;
    protected readonly MongoDbService _dbService;

    public IntegrationTest(ApiWebApplicationFactory fixture)
    {
        _factory = fixture;
        _factory.CreateClient();

        _dbService = fixture.DbService;
        _client = _factory.CreateClient();

        Console.WriteLine("EEELo!");
    }

    public void Dispose()
    {
        _dbService.GetClient().DropDatabase(
            _dbService.GetDatabase().DatabaseNamespace.DatabaseName
        );
    }
}
