using AutoWarden.Core.Models;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Request;

/// <summary>
/// Request model for Shell Action Definition (Create).
/// </summary>
public class ShellActionDefinitionCreateDto : IModelOf<ShellActionDefinition>
{
    /// <summary>
    /// Id. Must be unique within the system.
    /// </summary>
    /// <example>install-docker</example>
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

    public ShellActionDefinitionBodyCreateDto Body { get; set; } = null!;
}

public class ShellActionDefinitionBodyCreateDto : IModelOf<ShellActionDefinitionBody>
{
    /// <summary>
    /// Script to execute on server.
    /// </summary>
    /// <example>apt-get install docker</example>
    public string Script { get; set; } = null!;
}
