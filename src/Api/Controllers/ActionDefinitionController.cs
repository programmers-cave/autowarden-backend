using AutoMapper;
using AutoWarden.Api.Models.Request;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoWarden.Api.Controllers;

[Controller]
[Route("/api/action-definition")]
[Tags("xyz")]
public class ActionDefinitionController : CrudBaseController<ActionDefinition, string, ActionDefinitionRequestModel, ActionDefinitionResponseModel>
{
    public ActionDefinitionController(IActionDefinitionRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
