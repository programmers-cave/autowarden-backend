using AutoMapper;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Database.Entities.Mappings;

public class ActionDefinitionMappingProfile : Profile
{
    public ActionDefinitionMappingProfile()
    {
        CreateMap<ActionDefinition, ActionDefinitionEntity>();
        CreateMap<ActionDefinitionEntity, ActionDefinition>();
    }    
}
