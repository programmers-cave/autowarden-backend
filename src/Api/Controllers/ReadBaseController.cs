using AutoMapper;
using AutoWarden.Api.Models.Response;
using AutoWarden.Core;
using AutoWarden.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoWarden.Api.Controllers;

/**
 * To describe the controller in Swagger, you can use special magic variables, resolved by GenericDescriberOperationFilter.
 * Examples for OpenAPI tag named "SampleName":
 * - [ControllerName] = SampleName
 * - [ControllerNameVerbose] = Sample Name
 *
 * You can override default tag using [Tag("ControllerName")] attribute on controller class.
 */
[Consumes("application/json")]
[Produces("application/json")]
public class ReadBaseController<TModel, TIndex, TItemModel, TCollectionModel> : ControllerBase
      where TIndex : IEquatable<TIndex>
      where TModel : class, IIndexableObject<TIndex>, new()
      where TItemModel : class, IModelOf<TModel>, new()
      where TCollectionModel : class, IModelOf<TModel>, new()
{
    private protected readonly IReadOnlyRepository<TModel, TIndex> _repository;
    private protected readonly IMapper _mapper;

    public ReadBaseController(IReadOnlyRepository<TModel, TIndex> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [
        SwaggerOperation(
            Summary = "Get [ControllerNameVerbose] collection.",
            Description = "Gets [ControllerNameVerbose] collection.",
            OperationId = "[ControllerName]:GetCollection"
        ),
        SwaggerResponse(StatusCodes.Status200OK, "[ControllerNameVerbose] collection"),
    ]
    public async Task<ActionResult<PaginatedResponse<TCollectionModel>>> GetCollection(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var totalCount = await _repository.GetTotalCountAsync();

        var response = new PaginatedResponse<TCollectionModel>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int) Math.Ceiling((double) totalCount / pageSize),
            Data = _mapper.Map<List<TCollectionModel>>(await _repository.GetRangeAsync((page - 1) * pageSize, pageSize))
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    [
        SwaggerOperation(
            Summary = "Get single [ControllerNameVerbose] item.",
            Description = "Gets single [ControllerNameVerbose] item.",
            OperationId = "[ControllerName]:Get"
        ),
        SwaggerResponse(StatusCodes.Status200OK, "[ControllerNameVerbose] found"),
        SwaggerResponse(StatusCodes.Status404NotFound, "[ControllerNameVerbose] not found")
    ]
    public async Task<ActionResult<TItemModel>> Get(
        [FromRoute, SwaggerParameter("id")] TIndex id
    )
    {
        var obj = await _repository.GetByIdAsync(id);

        return Ok(_mapper.Map<TItemModel>(obj));
    }
}
