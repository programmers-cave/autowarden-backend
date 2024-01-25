using AutoMapper;
using AutoWarden.Api.Models.Request;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Mappings;

public class ActionDefinitionMappingProfile : Profile
{
    public ActionDefinitionMappingProfile()
    {
        // ActionDefinition
        // RESPONSE => ENTITY
        // ENTITY => RESPONSE
        CreateMap<ActionDefinition, ActionDefinitionGetDto>();
        CreateMap<ActionDefinition, ActionDefinitionGetCollectionDto>();

        // ShellActionDefinition
        // REQUEST => ENTITY
        CreateMap<ShellActionDefinitionCreateDto, ShellActionDefinition>();
        CreateMap<ShellActionDefinitionBodyCreateDto, ShellActionDefinitionBody>();

        CreateMap<ShellActionDefinitionUpdateDto, ShellActionDefinition>();
        CreateMap<ShellActionDefinitionBodyUpdateDto, ShellActionDefinitionBody>();
        // ENTITY => RESPONSE
        CreateMap<ShellActionDefinition, ShellActionDefinitionGetDto>();
        CreateMap<ShellActionDefinitionBody, ShellActionDefinitionBodyGetDto>();

        CreateMap<ShellActionDefinition, ShellActionDefinitionGetCollectionDto>();
        CreateMap<ShellActionDefinitionBody, ShellActionDefinitionBodyGetCollectionDto>();
    }
}
