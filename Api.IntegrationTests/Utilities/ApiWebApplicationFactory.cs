using AutoWarden.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoWarden.Api.IntegrationTests.Utilities;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    public IConfiguration Configuration { get; private set; }
    public MongoDbService DbService { get; private set; }

    private MongoDbSettings _mongoDbSettings;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();

            _mongoDbSettings = Configuration.GetSection("MongoDB").Get<MongoDbSettings>()!;
            _mongoDbSettings.DatabaseName = _mongoDbSettings.DatabaseName + "_" + "testdb"; // Guid.NewGuid();

            config.AddConfiguration(Configuration);
        });

        builder.ConfigureTestServices(services =>
        {
            DbService = new MongoDbService(_mongoDbSettings);
            services.AddSingleton<MongoDbService>(service => DbService);
        });
    }
}
