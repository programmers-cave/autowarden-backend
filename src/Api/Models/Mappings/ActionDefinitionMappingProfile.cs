using AutoMapper;
using AutoWarden.Api.Models.Request;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Mappings;

public class ActionDefinitionMappingProfile : Profile
{
    public ActionDefinitionMappingProfile()
    {
        CreateMap<ActionDefinitionRequestModel, ActionDefinition>();
        CreateMap<ActionDefinition, ActionDefinitionResponseModel>();
    }
}
