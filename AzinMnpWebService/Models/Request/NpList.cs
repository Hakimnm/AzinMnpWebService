namespace AzinMnpWebService.Models.Request;

public class NpList
{
    public string? CustomerName { get; set; }
    public string? CustomerId { get; set; }
    public string? RecipientContactInfo { get; set; }
    public int MessageTypeId { get; set; }
    public int SubscriptionType { get; set; }
    public int SenderId { get; set; }
    public int NumberType { get; set; }
    public int CustomerType { get; set; }
    public string? PlaceOfBirth { get; set; }
}