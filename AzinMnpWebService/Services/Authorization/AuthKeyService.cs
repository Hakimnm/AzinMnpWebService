using System.Security.Cryptography;
using AzinMnpWebService.Common;
using AzinMnpWebService.Models.Request.AuthKey;
using AzinMnpWebService.Repositories;

namespace AzinMnpWebService.Services.Authorization;

public class AuthKeyService : IAuthKeyService
{
    private readonly IMongoRepository _mongoRepository;
    private readonly string _requestLogCollection;

    public AuthKeyService(IMongoRepository mongoRepository)
    {
        _mongoRepository = mongoRepository;
        _requestLogCollection = "auth_key";
    }

    public async Task<string> GenerateAuthKey(string prefix)
    {
        var randomBytes = new byte[128];
        RandomNumberGenerator.Fill(randomBytes);
        var randomPart = Convert.ToBase64String(randomBytes)
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");
        return await Task.Run(() => $"{prefix}{randomPart}");
    }
    public async Task<CreateAuthKey> Create(RequestAuthKey request)
    {
        var authkey = new CreateAuthKey { AuthKey = Extension.SHA1Special(request.AuthKey!), IsActive = true, CreatedAt = DateTime.Now };
        return await _mongoRepository.InsertRecordAsync(_requestLogCollection, authkey);
    }

    public async Task<CreateAuthKey> Update(Guid id, RequestAuthKey request)
    {
        return await _mongoRepository.UpdateAuthKeyAsync<CreateAuthKey>(_requestLogCollection, id, request.AuthKey!);
    }

    public async Task<CreateAuthKey> Delete(Guid id)
    {
        return await _mongoRepository.DeleteAuthKeyAsync<CreateAuthKey>(_requestLogCollection, id);
    }

    public async Task<List<CreateAuthKey>> AllAuthKey()
    {
        return await _mongoRepository.LoadRecordsAsync<CreateAuthKey>(_requestLogCollection);
    }
}