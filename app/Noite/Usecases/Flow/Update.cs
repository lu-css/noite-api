using Noite.Models;
using Noite.Repositories;

namespace Noite.Usecases.Flow;

public class Update
{
    private readonly FlowRepository _flowRepository;

    public Update(FlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }

    public async Task Execute(string userId, Guid flowId, List<TableNodeModel> table)
    {
        await _flowRepository.UpdateAsync(userId, flowId, table);
    }
}
