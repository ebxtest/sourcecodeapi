using SourceCodeApi.Model;

namespace SourceCodeApi.Client;

public interface ISourceCodeClient
{
    Task<IEnumerable<SourceCodeCommit>> GetAllCommits(string owner, string repository, CancellationToken cancellationToken);
}