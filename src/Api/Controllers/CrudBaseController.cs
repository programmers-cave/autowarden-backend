using AutoMapper;
using AutoWarden.Api.Json;
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
public class CrudBaseController<TModel, TIndex, TItemModel, TCollectionModel, TCreateModel, TUpdateModel> : ReadBaseController<TModel, TIndex, TItemModel, TCollectionModel>
    where TIndex : IEquatable<TIndex>
    where TModel : class, IIndexableObject<TIndex>, new()
    where TItemModel : class, IModelOf<TModel>, new()
    where TCollectionModel : class, IModelOf<TModel>, new()
    where TCreateModel : class, IModelOf<TModel>, new()
    where TUpdateModel : class, IModelOf<TModel>, new()
{
    private protected readonly IRepository<TModel, TIndex> _repository;
    
    public CrudBaseController(IRepository<TModel, TIndex> repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
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
    public async Task<ActionResult<TItemModel>> Post([FromBody] TCreateModel requestModel)
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
    public async Task<ActionResult<TItemModel>> Patch(
        [FromRoute, SwaggerParameter("Id")] TIndex id,
        [FromBody] MergePatchJson<TUpdateModel> requestModelJson
    )
    {
        var objToUpdate = await _repository.GetByIdAsync(id);
        var baseModel = ModelPatcher.Patch(objToUpdate, requestModelJson);
        await _repository.ReplaceByIdAsync(id, baseModel);
        
        return CreatedAtAction(
            nameof(Get),
            new {id},
            _mapper.Map<TItemModel>(baseModel)
        );
    }
    
    [HttpDelete("{id}")]
    [
        SwaggerOperation(
            Summary = "Delete [ControllerNameVerbose] item.",
            Description = "Deletes [ControllerNameVerbose] item.",
            OperationId = "[ControllerName]:Delete"
        ),
        SwaggerResponse(StatusCodes.Status204NoContent, "[ControllerNameVerbose] deleted"),
        SwaggerResponse(StatusCodes.Status404NotFound, "[ControllerNameVerbose] not found")
    ]
    public async Task<ActionResult> Delete(
        [FromRoute, SwaggerParameter("Id")] TIndex id
    )
    {
        await _repository.DeleteByIdAsync(id);
        
        return NoContent();
    }
}
