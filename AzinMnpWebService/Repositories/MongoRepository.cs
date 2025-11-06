using AzinMnpWebService.Common;
using AzinMnpWebService.Models.Request.AuthKey;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AzinMnpWebService.Repositories;

public class MongoRepository : IMongoRepository
{
    private readonly IMongoDatabase _database;

    public MongoRepository()
    {
        var connectionstring = "mongodb://localhost:27017/";
        var dbName = "clients";
        var client = new MongoClient(connectionstring);
        _database = client.GetDatabase(dbName);
    }

    public async Task<T> InsertAuthKeyAsync<T>(string table, T record)
    {
        var collection = _database.GetCollection<T>(table);
        await collection.InsertOneAsync(record);
        return record;
    }

    public async Task<List<T>> LoadAuthKeyAsync<T>(string table)
    {
        var collection = _database.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("IsActive", true);

        return await collection.Find(filter).ToListAsync();
    }

    public async Task<bool> AuthKeyExistsAsync(string table,string authKey)
    {
        var collection = _database.GetCollection<CreateAuthKey>(table);
        var filter = Builders<CreateAuthKey>.Filter.Eq(x => x.AuthKey, authKey);
        var count = await collection.CountDocumentsAsync(filter);
        return count > 0;

    }

    public async Task<T> UpdateAuthKeyAsync<T>(string table, Guid id, string newKey)
    {
        var update = Builders<T>.Update.Set("AuthKey", Extension.SHA1Special(newKey));
        return (await FindByIdInternalAsync(table, id, update))!;
    }
    public async Task<T> DeleteAuthKeyAsync<T>(string table, Guid id)
    {
        var update = Builders<T>.Update.Set("IsActive", false);
        return (await FindByIdInternalAsync(table, id, update))!;
    }
    public async Task UpsertRecordAsync<T>(string table, Guid id, T record)
    {
        var collection = _database.GetCollection<T>(table);
        var filter = Builders<T>.Filter.Eq("_id", id.ToString());

        var result = await collection.ReplaceOneAsync(
            filter,
            record,
            new ReplaceOptions { IsUpsert = true });
    }

    private async Task<T?> FindByIdInternalAsync<T>(
        string table,
        Guid id,
        UpdateDefinition<T> update)
    {
        var collection = _database.GetCollection<T>(table);

        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq("Id", id),
            Builders<T>.Filter.Eq("IsActive", true)
        );

        var result = await collection.UpdateOneAsync(filter, update);

        if (result.MatchedCount == 0)
            throw new KeyNotFoundException($"Record with Id - '{id}' not found or inactive.");

        return await collection.Find(filter).FirstOrDefaultAsync();
    }
}