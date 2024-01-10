using AutoWarden.Api.Json;
using AutoWarden.Core.Models;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Request;

/// <summary>
/// Request model for Action Definition.
/// </summary>
public class ActionDefinitionRequestModel : IModelOf<ActionDefinition>
{
    /// <summary>
    /// Id. Must be unique within the system.
    /// </summary>
    /// <example>install-docker</example>
    [Unpatchable]
    public string Id { get; set; } = null!;
    
    /// <summary>
    /// Displayed name.
    /// </summary>
    /// <example>Install Docker</example>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Displayed description.
    /// </summary>
    /// <example>It can install docker on most Linux systems.</example>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// JSON body.
    /// </summary>
    /// <example>{"type": "shell", "inline": ["apt-get install docker"]}</example>
    public string Body { get; set; } = null!;
}
