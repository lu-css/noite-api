using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Noite.Models;

public class FlowModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid? Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    public AccessLevel AccessLevel { get; set; } = AccessLevel.Public;
    public List<string> CanAccess { get; set; } = new List<string>();
    public List<TableNodeModel>? SavedNodes { get; set; } = new List<TableNodeModel>();
}

public enum AccessLevel
{
    Public,
    Private
}
