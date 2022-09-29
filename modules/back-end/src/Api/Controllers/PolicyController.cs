using Application.Bases.Models;
using Application.Policies;

namespace Api.Controllers;

[Route("api/v{version:apiVersion}/organizations/{organizationId:guid}/policies")]
public class PolicyController : ApiControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ApiResponse<PolicyVm>> GetAsync(Guid id)
    {
        var request = new GetPolicy
        {
            Id = id
        };

        var policy = await Mediator.Send(request);
        return Ok(policy);
    }

    [HttpGet]
    public async Task<ApiResponse<PagedResult<PolicyVm>>> GetListAsync(
        Guid organizationId,
        [FromQuery] PolicyFilter filter)
    {
        var request = new GetPolicyList
        {
            OrganizationId = organizationId,
            Filter = filter
        };

        var policies = await Mediator.Send(request);
        return Ok(policies);
    }

    [HttpGet("is-name-used")]
    public async Task<ApiResponse<bool>> IsNameUsedAsync(Guid organizationId, string name)
    {
        var request = new IsPolicyNameUsed
        {
            OrganizationId = organizationId,
            Name = name
        };

        var isNameUsed = await Mediator.Send(request);
        return Ok(isNameUsed);
    }

    [HttpGet("{policyId:guid}/groups")]
    public async Task<ApiResponse<PagedResult<PolicyGroup>>> GetGroupsAsync(
        Guid organizationId,
        Guid policyId,
        [FromQuery] PolicyGroupFilter filter)
    {
        var request = new GetPolicyGroup
        {
            OrganizationId = organizationId,
            PolicyId = policyId,
            Filter = filter
        };

        var groups = await Mediator.Send(request);
        return Ok(groups);
    }

    [HttpGet("{policyId:guid}/members")]
    public async Task<ApiResponse<PagedResult<PolicyMember>>> GetMembersAsync(
        Guid organizationId,
        Guid policyId,
        [FromQuery] PolicyMemberFilter filter)
    {
        var request = new GetPolicyMember
        {
            OrganizationId = organizationId,
            PolicyId = policyId,
            Filter = filter
        };

        var members = await Mediator.Send(request);
        return Ok(members);
    }
}