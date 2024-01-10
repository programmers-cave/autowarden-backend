using AutoMapper;
using AutoWarden.Api.Json;
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
public class CrudBaseController<TModel, TIndex, TRequestModel, TResponseModel> : ControllerBase
    where TIndex : IEquatable<TIndex>
    where TModel : class, IIndexableObject<TIndex>, new()
    where TRequestModel : class, IModelOf<TModel>, new()
    where TResponseModel : class, IModelOf<TModel>, new()
{
    protected readonly IRepository<TModel, TIndex> _repository;
    protected readonly IMapper _mapper;
    
    public CrudBaseController(IRepository<TModel, TIndex> repository, IMapper mapper)
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
    public async Task<ActionResult<PaginatedResponse<TResponseModel>>> GetCollection(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var totalCount = await _repository.GetTotalCountAsync();

        var response = new PaginatedResponse<TResponseModel>
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int) Math.Ceiling((double) totalCount / pageSize),
            Data = _mapper.Map<List<TResponseModel>>(await _repository.GetRangeAsync((page - 1) * pageSize, pageSize))
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
    public async Task<ActionResult<TResponseModel>> Get(
        [FromRoute, SwaggerParameter("id")] TIndex id
    )
    {
        var obj = await _repository.GetByIdAsync(id);
        
        return Ok(_mapper.Map<TResponseModel>(obj));
    }
    
    [HttpPost]
    [
        SwaggerOperation(
            Summary = "Create [ControllerNameVerbose] item.",
            Description = "Creates new [ControllerNameVerbose] item",
            OperationId = "[ControllerName]:Post"
        ),
        SwaggerResponse(StatusCodes.Status201Created, "[ControllerNameVerbose] created")
    ]
    public async Task<ActionResult<TResponseModel>> Post([FromBody] TRequestModel requestModel)
    {
        var baseModel = _mapper.Map<TModel>(requestModel);
        await _repository.CreateAsync(baseModel);
        
        return CreatedAtAction(nameof(Get), new {id = baseModel.Id}, requestModel);
    }
    
    [HttpPatch("{id}")]
    [
        SwaggerOperation(
            Summary = "Update [ControllerNameVerbose] item.",
            Description = "Updates [ControllerNameVerbose] item.",
            OperationId = "[ControllerName]:Patch"
        ),
        SwaggerResponse(StatusCodes.Status200OK, "[ControllerNameVerbose] updated"),
        SwaggerResponse(StatusCodes.Status404NotFound, "[ControllerNameVerbose] not found")
    ]
    public async Task<ActionResult<TResponseModel>> Patch(
        [FromRoute, SwaggerParameter("Id")] TIndex id,
        [FromBody] MergePatchJson<TRequestModel> requestModelJson
    )
    {
        var objToUpdate = await _repository.GetByIdAsync(id);
        var baseModel = ModelPatcher.Patch(objToUpdate, requestModelJson);
        await _repository.ReplaceByIdAsync(id, baseModel);
        
        return CreatedAtAction(
            nameof(Get),
            new {id},
            _mapper.Map<ActionDefinitionResponseModel>(baseModel)
        );
    }
    
    [HttpDelete]
    [
        SwaggerOperation(
            Summary = "Delete [ControllerNameVerbose] item.",
            Description = "Deletes [ControllerNameVerbose] item.",
            OperationId = "[ControllerName]:Delete"
        ),
        SwaggerResponse(StatusCodes.Status204NoContent, "[ControllerNameVerbose] deleted"),
        SwaggerResponse(StatusCodes.Status404NotFound, "[ControllerNameVerbose] not found")
    ]
    public async Task<ActionResult<TResponseModel>> Delete(
        [FromRoute, SwaggerParameter("Id")] TIndex id
    )
    {
        await _repository.DeleteByIdAsync(id);
        
        return NoContent();
    }
}
