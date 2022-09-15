using Octokit;
using SourceCodeApi.Model;

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

    public async Task<List<ContributorCommit>> GetLatestContributorCommits(string owner, string repository, int maxResult, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Getting latest {maxResult} commits from GitHub repository {repository}");

        var result = await _gitHubClient.Repository.Commit.GetAll(owner, repository);

        return result.OrderByDescending(x => x.Commit.Committer.Date.Date).Take(maxResult).
            Select(x => new ContributorCommit
            {
                ContributorEmail = x.Commit.Author.Name,
                CommitTimeStamp = x.Commit.Author.Date.Date
            }).ToList();
    }
}