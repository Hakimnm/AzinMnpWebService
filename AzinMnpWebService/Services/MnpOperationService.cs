using AzinMnpWebService.Models.Request;
using AzinMnpWebService.Models.Response;

namespace AzinMnpWebService.Services;

public class MnpOperationService:IMnpOperationService
{
    public async Task<string> CheckRequestStatus(RequestMnpStatus request)
    {

        return  "Melumat yoxlanilir.";
    }
    
    public async Task<ResponsePhoneStatus> CheckPhoneStatus(RequestPinPhone request)
    {

        return  new ResponsePhoneStatus(){Phone = "518881507",OperatorName = "Bakcell",TransportDate= "02-02-2022"};
    }
}