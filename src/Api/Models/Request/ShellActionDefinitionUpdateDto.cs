using AutoWarden.Core.Models;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Request;

/// <summary>
/// Request model for Shell Action Definition (Update).
/// </summary>
public class ShellActionDefinitionUpdateDto : IModelOf<ShellActionDefinition>
{
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

    public ShellActionDefinitionBodyUpdateDto Body { get; set; } = null!;
}

public class ShellActionDefinitionBodyUpdateDto : IModelOf<ShellActionDefinitionBody>
{
    /// <summary>
    /// Script to execute on server.
    /// </summary>
    /// <example>apt-get install docker</example>
    public string Script { get; set; } = null!;
}
