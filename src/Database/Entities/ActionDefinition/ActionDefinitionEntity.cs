using AutoWarden.Database.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoWarden.Database.Entities.ActionDefinition;

[BsonCollection("ActionDefinitions")]
[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(ShellActionDefinitionEntity))]
public class ActionDefinitionEntity : IEntity<string>
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
