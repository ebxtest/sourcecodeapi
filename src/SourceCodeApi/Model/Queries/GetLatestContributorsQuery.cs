using MediatR;

namespace SourceCodeApi.Model.Queries;

public class GetLatestContributorsQuery : IRequest<List<ContributorCommit>>
{
    public string Repository { get; set; }
    public string RepositoryOwner { get; set; }
    public int MaxResult { get; set; }
}