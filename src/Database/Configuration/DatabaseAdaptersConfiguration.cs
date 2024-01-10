using System.Reflection;
using AutoWarden.Database.Repositories;
using AutoWarden.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace AutoWarden.Database.Configuration;

public static class DatabaseAdaptersConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, MongoDbSettings dbSettings)
    {
        // Internal required services
        services.AddSingleton<MongoDbService>(service => new MongoDbService(dbSettings));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Repositories
        services.AddTransient<IActionDefinitionRepository, ActionDefinitionRepository>();
        
        return services;
    }
}
