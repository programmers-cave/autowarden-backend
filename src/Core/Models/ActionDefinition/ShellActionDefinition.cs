namespace AutoWarden.Core.Models.ActionDefinition;

public class ShellActionDefinition : ActionDefinition
{
    public new ShellActionDefinitionBody Body { get; set; } = null!;
}

public class ShellActionDefinitionBody : ActionDefinitionBody
{
    public string Script { get; set; } = null!;
}
