using AutoMapper;
using AutoWarden.Database.Entities;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Core.Repositories;

namespace AutoWarden.Database.Repositories;

public class ActionDefinitionRepository : Repository<ActionDefinition, ActionDefinitionEntity, string>, IActionDefinitionRepository
{
    public ActionDefinitionRepository(IMapper mapper, MongoDbService mongoDbService) : base(mapper, mongoDbService)
    {
    }
}
