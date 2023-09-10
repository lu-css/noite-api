using Noite.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Noite.Settings;

namespace Noite.Repositories;

public class FlowRepository
{
    private readonly IMongoCollection<UserModel> _userCollection;
    private readonly UserRepository _userRepository;

    public FlowRepository(IOptions<NoiteDatabaseSettings> databaseSettings)
    {
        _userRepository = new UserRepository(databaseSettings);
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase
          .GetCollection<UserModel>("User");
    }

    public async Task<FlowModel?> GetAsync(string userId, Guid flowId)
    {
        var user = await _userRepository.GetAsync(userId) ?? throw new Exception("User not founded");

        return user.flows.Find(flow => flow.Id == flowId);
    }

    public async Task<List<FlowModel>> ListFromUser(string userId)
    {
        var user = await _userRepository.GetAsync(userId) ?? throw new Exception("User not founded");

        return user.flows;
    }

    public async Task CreateAsync(string id, FlowModel newFlow)
    {
        var user = await _userRepository.GetAsync(id) ?? throw new Exception("User not found");
        newFlow.Id = Guid.NewGuid();

        if (user.flows == null)
        {
            user.flows = new List<FlowModel>() { newFlow };
        }
        else
        {
            user.flows.Add(newFlow);
        }

        await _userRepository.UpdateAsync(id, user);
    }

    public async Task UpdateAsync(string userId, Guid flowId, List<TableNodeModel> table)
    {
        var user = await _userRepository.GetAsync(userId) ?? throw new Exception("User not found");

        var index = user.flows.FindIndex(f => f.Id == flowId);
        user.flows[index].SavedNodes = table;

        await _userRepository.UpdateAsync(userId, user);
    }
}
