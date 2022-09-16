using Octokit;

namespace SourceCodeApi.Client;

public class GitHubSourceCodeClient: ISourceCodeClient
{
    private readonly IGitHubClient _gitHubClient;
    private readonly ILogger<GitHubSourceCodeClient> _logger;

    public GitHubSourceCodeClient(IGitHubClient gitHubClient, ILogger<GitHubSourceCodeClient> logger)
    {
        _gitHubClient = gitHubClient;
        _logger = logger;
    }

    public async Task<IEnumerable<SourceCodeCommit>> GetAllCommits(string owner, string repository, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting commits from GitHub repository {repository}");

        var result = await _gitHubClient.Repository.Commit.GetAll(owner, repository);

        return result.Select(x => new SourceCodeCommit
        {
            AuthorName = x.Commit.Author.Name,
            TimeStamp = x.Commit.Author.Date.DateTime,
            Id = x.Commit.Tree.Sha,
            Message = x.Commit.Message
        });
    }
}