using AutoMapper;
using AutoWarden.Api.Models.Request;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core.Models.ActionDefinition;
using AutoWarden.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoWarden.Api.Controllers;

[Controller]
[Route("/api/action-definitions/shell")]
public class ShellActionDefinitionController : CrudBaseController<ShellActionDefinition, string,
    ShellActionDefinitionGetDto, ShellActionDefinitionGetCollectionDto,
    ShellActionDefinitionCreateDto, ShellActionDefinitionUpdateDto>
{
    public ShellActionDefinitionController(IShellActionDefinitionRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
