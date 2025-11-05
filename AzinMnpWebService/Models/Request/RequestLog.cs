using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzinMnpWebService.Models.Request;

public class RequestLog
{
    [BsonId]
    [BsonRepresentation(BsonType.String)] public Guid Id { get; set; }
    public string? RequestUrl { get; set; }
    public string? RequestHeaders { get; set; }
    public int ResponseStatusCode { get; set; }
    public string? ResponseBody { get; set; }
    public string? ResponseHeaders { get; set; }
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public string? RequestIp { get; set; }
    public string?  RequestDuration  { get; set; }
}