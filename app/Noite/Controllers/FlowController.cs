using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Noite.Repositories;
using Noite.Models;
using Noite.Helpers;

using Flow = Noite.Usecases.Flow;

namespace Noite.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FlowController : ControllerBase
{
    private readonly FlowRepository _flowRepository;

    public FlowController(FlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }

    [HttpGet("{flowId:Guid}")]
    public async Task<ActionResult<FlowModel>> Get(Guid flowId)
    {
        string userId = AuthHelper.UserId(User);
        var flow = await _flowRepository.GetAsync(userId, flowId);

        if (flow is null)
        {
            return NotFound("Flow not found");
        }

        return flow;
    }

    [HttpGet("MyFlows")]
    public async Task<List<FlowModel>> ListMyFlows()
    {
        string userId = AuthHelper.UserId(User);
        var flows = await _flowRepository.ListFromUser(userId);
        return flows;
    }

    [HttpPost("New")]
    public async Task<ActionResult<FlowModel>> Create(FlowModel newFlow)
    {
        string userId = AuthHelper.UserId(User);
        return await new Flow.Create(_flowRepository).Execute(userId, newFlow.Name, newFlow.Description, newFlow.AccessLevel);
    }

    [HttpPut("{flowId:Guid}")]
    public async Task<IActionResult> Update(Guid flowId, List<TableNodeModel> tableNode)
    {
        string userId = AuthHelper.UserId(User);
        await new Flow.Update(_flowRepository).Execute(userId, flowId, tableNode);

        return Ok("Deu bom");
    }
}
