using AzinMnpWebService.Models.Request.AuthKey;

namespace AzinMnpWebService.Services.Authorization;

public interface IAuthKeyService
{
    Task<string> GenerateAuthKey(string prefix);
    Task<bool> ValidateAuthKeyAsync(string key);
    Task<CreateAuthKey> Create(RequestAuthKey request);
    Task<List<CreateAuthKey>> AllAuthKey();
    Task<CreateAuthKey> Update(Guid id,RequestAuthKey request);
    Task<CreateAuthKey> Delete(Guid id);
}