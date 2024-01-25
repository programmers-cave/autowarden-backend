namespace AutoWarden.Database.Entities.ActionDefinition;

public class ShellActionDefinitionEntity : ActionDefinitionEntity
{
    public ShellActionDefinitionEntityBody Body { get; set; } = null!;
}

public class ShellActionDefinitionEntityBody
{
    public string Script { get; set; } = null!;
}
