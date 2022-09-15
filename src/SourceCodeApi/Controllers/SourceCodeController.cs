using MediatR;
using Microsoft.AspNetCore.Mvc;
using SourceCodeApi.Handlers;
using SourceCodeApi.Model;
using SourceCodeApi.Model.Queries;

namespace SourceCodeApi.Controllers;

[Route("api/v1")]
[ApiController]
public class SourceCodeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SourceCodeController> _logger;

    public SourceCodeController(IMediator mediator, ILogger<SourceCodeController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [ProducesResponseType(typeof(GetCommitsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{owner}/{repo}/contributors")]
    public async Task<IActionResult> GetLatestCommits(string owner, string repo, int maxResult = 30)
    {
        var result = await _mediator.Send(new GetLatestContributorsQuery
        {
            Repository = repo,
            RepositoryOwner = owner,
            MaxResult = maxResult
        });

        return Ok(result);
    }
}