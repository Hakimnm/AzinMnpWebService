namespace AzinMnpWebService.Models.Request;

public class Attachment
{
    public string? FileName { get; set; }
    public string? FileType { get; set; }
    public IFormFile ? Data { get; set; }
}