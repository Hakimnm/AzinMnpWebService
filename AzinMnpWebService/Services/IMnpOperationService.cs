using AzinMnpWebService.Models.Request;
using AzinMnpWebService.Models.Response;

namespace AzinMnpWebService.Services;

public interface IMnpOperationService
{
    Task<string> CheckRequestStatus(RequestMnpStatus request);
    Task<ResponsePhoneStatus> CheckPhoneStatus(RequestPinPhone request);
}