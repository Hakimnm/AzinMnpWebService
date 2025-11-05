namespace AzinMnpWebService.Repositories;

public interface IMongoRepository
{
    public Task<T> InsertRecordAsync<T>(string table, T record);
    public Task<List<T>> LoadRecordsAsync<T>(string table);
    public Task<T> UpdateAuthKeyAsync<T>(string table, Guid id,string record);
    public Task<T> DeleteAuthKeyAsync<T>(string table, Guid id);
    public Task UpsertRecordAsync<T>(string table, Guid id, T record);
}