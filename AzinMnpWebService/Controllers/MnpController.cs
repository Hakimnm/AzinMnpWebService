using AzinMnpWebService.Filters;
using AzinMnpWebService.Models.Request;
using AzinMnpWebService.Models.Response;
using AzinMnpWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzinMnpWebService.Controllers;

[AuthKeyRequired]
[ServiceFilter(typeof(AuthKeyValidationFilter))]
public class MnpController : ApiController
{
    private readonly MnpSoapService _soapService;
    private readonly IMnpOperationService _mnpOperationService;

    public MnpController(MnpSoapService soapService,IMnpOperationService mnpOperationService)
    {
        _soapService = soapService;
        _mnpOperationService = mnpOperationService;
    }

    [HttpPost, Route("/CreateRequest")]
    public async Task<SoapResponse.Envelope> MnpCreateRequest()
    {
        string soapRequest = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""
               xmlns:tns=""NPCDB"">
  <soap:Body>
    <tns:ProcessMessage>
      <tns:xmlMessage><![CDATA[
      
      <NPMessages>
        <PortMessages>
          <PortMessage>
            <MessageCode>NP Request</MessageCode>
            <NPRequestId>24bfad86-6e94-4c97-a4da-6d929383cf86</NPRequestId>
            <ProcessType>MobilePort</ProcessType>
            <RecipientId>11</RecipientId>
            <NewRoute>D100</NewRoute>
            <NPDueDate>2025-11-04T00:00:00</NPDueDate>
            <Numbers>
              <NumberRange>
                <NumberFrom>994999944449</NumberFrom>
              </NumberRange>
            </Numbers>

            <NpList>
              <CustomerName>Vasif Qəribov Rasim Oğlu</CustomerName>
              <CustomerID>1994-02-08</CustomerID>
              <RecipientContactInfo>999944449</RecipientContactInfo>
              <MessageTypeId>1</MessageTypeId>
              <SubscriptionType>0</SubscriptionType>
              <SenderId>11</SenderId>
              <NumberType>2</NumberType>
              <CustomerType>0</CustomerType>
              <PlaceOfBirth>ƏLİ-BAYRAMLI</PlaceOfBirth>
            </NpList>
          </PortMessage>
        </PortMessages>
      </NPMessages>

      ]]></tns:xmlMessage>
    </tns:ProcessMessage>
  </soap:Body>
</soap:Envelope>";

        var res = await _soapService.CreateRequest(soapRequest);
        return res;
    }

    [HttpGet, Route("/CheckRequestStatus")]
    public async Task<string> CheckRequestStatus([FromQuery] RequestMnpStatus request)
    {
        return await _mnpOperationService.CheckRequestStatus(request);
    }
    
    [HttpGet, Route("/CheckPhoneStatus")]
    public async Task<ResponsePhoneStatus> CheckPhoneStatus([FromQuery]RequestPinPhone request)
    {
      return await _mnpOperationService.CheckPhoneStatus(request);
    }
}