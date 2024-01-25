using AutoMapper;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Database.Entities.ActionDefinition;

namespace AutoWarden.Database.Entities.Mappings;

public class ActionDefinitionMappingProfile : Profile
{
    public ActionDefinitionMappingProfile()
    {
        CreateMap<ActionDefinitionEntity, Core.Models.ActionDefinition.ActionDefinition>();

        CreateMap<ShellActionDefinition, ShellActionDefinitionEntity>();
        CreateMap<ShellActionDefinitionEntity, ShellActionDefinition>();

        CreateMap<ShellActionDefinitionEntityBody, ShellActionDefinitionBody>();
        CreateMap<ShellActionDefinitionBody, ShellActionDefinitionEntityBody>();
    }    
}
