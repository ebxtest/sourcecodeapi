using MediatR;
using SourceCodeApi.Client;
using SourceCodeApi.Model;
using SourceCodeApi.Model.Queries;

namespace SourceCodeApi.Handlers;

public class GetLatestContributorsQueryHandler : IRequestHandler<GetLatestContributorsQuery, List<ContributorCommit>>
{
    private readonly ISourceCodeClient _sourceCodeClient;
    private readonly ILogger<GetLatestContributorsQueryHandler> _logger;

    public GetLatestContributorsQueryHandler(ISourceCodeClient sourceCodeClient, ILogger<GetLatestContributorsQueryHandler> logger)
    {
        _sourceCodeClient = sourceCodeClient;
        _logger = logger;
    }

    public async Task<List<ContributorCommit>> Handle(GetLatestContributorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _sourceCodeClient.GetAllCommits(
            request.RepositoryOwner, 
            request.Repository,
            cancellationToken);

        var latestCommits = result
            .Select(x => new ContributorCommit
            {
                Contributor = x.AuthorName,
                CommitTimeStamp = x.TimeStamp,
                CommitId = x.Id
            })
            .OrderByDescending(x => x.CommitTimeStamp)
            .Take(request.MaxResult)
            .ToList();

        return latestCommits;
    }
}