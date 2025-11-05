using System.Net.Http.Headers;
using System.Text;
using AzinMnpWebService.Models.Response;

namespace AzinMnpWebService.Services;

public class MnpSoapService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly XmlConvertToModel _convertToModel;

    public MnpSoapService(IHttpClientFactory httpClientFactory, XmlConvertToModel convertToModel)
    {
        _httpClientFactory = httpClientFactory;
        _convertToModel = convertToModel;
    }

   public async Task< SoapResponse.Envelope> CreateRequest(string request)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var requestContent = new StringContent(request, Encoding.UTF8, "text/xml");
        var username =  "Azercell_Tmp_SOAP2";
        var password = "251a27a4c5aec5076b467f2ef46b9405";
        var url ="https://test-soap.nds.az:8080/npservice.svc";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        httpClient.BaseAddress = new Uri($"{url}");
        httpClient.DefaultRequestHeaders.Add("SOAPAction","NPCDB/NPService/ProcessMessage");
        var response = await httpClient.PostAsync($"{url}", requestContent);
        
        var responseContent = await response.Content.ReadAsStringAsync();
        var rr =await _convertToModel.CustomXmlConvertToModel(responseContent,new SoapResponse.Envelope());

        return rr;
    }




}