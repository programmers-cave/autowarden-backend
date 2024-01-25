namespace AutoWarden.Core.Models.ActionDefinition;

public class ActionDefinition : IIndexableObject<string>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ActionDefinitionBody Body { get; set; } = null!;
}

public class ActionDefinitionBody
{

}
