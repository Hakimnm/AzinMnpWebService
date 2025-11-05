namespace AzinMnpWebService.Models.Request;

public class PortMessage
{
    public string? MessageCode { get; set; }
    public string? NpRequestId { get; set; }
    public string? ProcessType { get; set; }
    public int RecipientId { get; set; }
    public string? NewRoute { get; set; }
    public DateTime NpDueDate { get; set; }
    public List<NumberRange>? Numbers { get; set; }
    public List<Attachment>? Attachments { get; set; }
    public List<NpList>? NpList { get; set; }
}