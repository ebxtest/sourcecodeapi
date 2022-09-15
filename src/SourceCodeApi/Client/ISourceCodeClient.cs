using SourceCodeApi.Model;

namespace SourceCodeApi.Client;

public interface ISourceCodeClient
{
    Task<List<ContributorCommit>> GetLatestContributorCommits(string owner, string repository, int maxResult, CancellationToken cancellationToken);
}