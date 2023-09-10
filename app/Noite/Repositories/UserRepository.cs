using Noite.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Noite.Settings;

namespace Noite.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<UserModel> _userCollection;

    public UserRepository(IOptions<NoiteDatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase
          .GetCollection<UserModel>("User");
    }

    public async Task<List<UserModel>> GetAsync()
    {
        return await _userCollection.Find(_ => true).ToListAsync();
    }

    public async Task<UserModel?> GetAsync(string id)
    {
        return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<UserModel> GetByEmail(string email)
    {
        return await _userCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(UserModel newUser)
    {
        await _userCollection.InsertOneAsync(newUser);
    }

    public async Task<UserModel?> UpdateAsync(string id, UserModel updatedUser)
    {
        var user = await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        return await GetAsync(id);
    }

    public async Task RemoveAsync(string id)
    {
        await _userCollection.DeleteOneAsync(x => x.Id == id);
    }
}
