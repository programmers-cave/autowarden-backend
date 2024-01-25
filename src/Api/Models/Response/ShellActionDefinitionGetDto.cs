using AutoWarden.Core.Models;
using AutoWarden.Core.Models.ActionDefinition;

namespace AutoWarden.Api.Models.Response;

/// <summary>
/// Response model for Shell Action Definition (Get Item).
/// </summary>
public class ShellActionDefinitionGetDto : IModelOf<ShellActionDefinition>
{
    /// <example>install-docker</example>
    public string Id { get; set; } = null!;

    /// <example>Install Docker</example>
    public string Name { get; set; } = null!;

    /// <example>It can install docker on most Linux systems.</example>
    public string Description { get; set; } = null!;

    public ShellActionDefinitionBodyGetDto Body { get; set; } = null!;
}

public class ShellActionDefinitionBodyGetDto : IModelOf<ShellActionDefinitionBody>
{
    /// <example>apt-get install -y docker</example>
    public string Script { get; set; } = null!;
}
