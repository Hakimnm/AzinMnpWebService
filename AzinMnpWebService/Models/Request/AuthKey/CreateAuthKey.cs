using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzinMnpWebService.Models.Request.AuthKey;

public class CreateAuthKey
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public string? AuthKey { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}