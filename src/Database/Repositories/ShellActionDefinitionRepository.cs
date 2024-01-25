using AutoMapper;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Core.Repositories;
using AutoWarden.Database.Entities.ActionDefinition;
using AutoWarden.Database.Repositories.Base;

namespace AutoWarden.Database.Repositories;

public class ShellActionDefinitionRepository : Repository<ShellActionDefinition, ShellActionDefinitionEntity, string>, IShellActionDefinitionRepository
{
    public ShellActionDefinitionRepository(IMapper mapper, MongoDbService mongoDbService) : base(mapper, mongoDbService)
    {
    }
}
