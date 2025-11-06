namespace AzinMnpWebService.Repositories;

public interface IMongoRepository
{
    public Task<T> InsertAuthKeyAsync<T>(string table, T record);
    public Task<List<T>> LoadAuthKeyAsync<T>(string table);
    public Task<bool> AuthKeyExistsAsync(string table,string authKey);
    public Task<T> UpdateAuthKeyAsync<T>(string table, Guid id,string newKey);
    public Task<T> DeleteAuthKeyAsync<T>(string table, Guid id);
    public Task UpsertRecordAsync<T>(string table, Guid id, T record);
}