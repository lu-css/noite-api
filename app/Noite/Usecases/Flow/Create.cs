using Noite.Models;
using Noite.Repositories;

namespace Noite.Usecases.Flow;

public class Create
{
    private readonly FlowRepository _flowRepository;

    public Create(FlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }

    public async Task<FlowModel> Execute(string userId, string name, string description, AccessLevel access)
    {
        var newFlow = new FlowModel
        {
            Name = name,
            Description = description,
            AccessLevel = access
        };

        await _flowRepository.CreateAsync(userId, newFlow);

        return newFlow;
    }
}
