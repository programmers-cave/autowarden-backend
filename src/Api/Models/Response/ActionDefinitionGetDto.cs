using AutoWarden.Core.Models;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Response;

/// <summary>
/// Response model for Action Definition (Get Item).
/// </summary>
public class ActionDefinitionGetDto : IModelOf<ActionDefinition>
{
    /// <example>install-docker</example>
    public string Id { get; set; } = null!;

    /// <example>Install Docker</example>
    public string Name { get; set; } = null!;

    /// <example>It can install docker on most Linux systems.</example>
    public string Description { get; set; } = null!;
}
