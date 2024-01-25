using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Core.Repositories;

namespace AutoWarden.Api.Controllers;

[Controller]
[Route("/api/action-definitions")]
public class ActionDefinitionController : ReadBaseController<ActionDefinition, string,
    ActionDefinitionGetDto, ActionDefinitionGetCollectionDto>
{
    public ActionDefinitionController(IActionDefinitionRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
