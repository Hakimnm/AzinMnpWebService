using System.Security.Cryptography;
using AzinMnpWebService.Models.Request.AuthKey;
using AzinMnpWebService.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzinMnpWebService.Controllers;

public class AuthorizationController: ApiController
{
    private readonly IAuthKeyService _authKeyService;
    public AuthorizationController(IAuthKeyService authKeyService)
    {
        _authKeyService = authKeyService;
    }
    [HttpGet,Route("/generateAuthKey")]
    public async Task<string> GenerateAuthKey([FromQuery] string prefix)
    {
        return await _authKeyService.GenerateAuthKey(prefix);
    }
    
    [HttpGet]
    public async Task<List<CreateAuthKey>> GetList()
    {
        var result = await _authKeyService.AllAuthKey();
        return result;
    }
    [HttpPost]
    public async Task<CreateAuthKey> CerateAuthKey([FromBody] RequestAuthKey requestAuthKey)
    {
        var result = await _authKeyService.Create(requestAuthKey);
        return result;
    }
    [HttpPut]
    public async Task<CreateAuthKey> UpdateAuthKey([FromQuery] Guid id ,[FromBody] RequestAuthKey requestAuthKey)
    {
        var result = await _authKeyService.Update(id,requestAuthKey);
        return result;
    }
    
    [HttpDelete]
    public async Task<CreateAuthKey> DeleteAuthKey([FromQuery] Guid id)
    {
        var result = await _authKeyService.Delete(id);
        return result;
    }
}