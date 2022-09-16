using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SourceCodeApi.Client;
using SourceCodeApi.Handlers;
using SourceCodeApi.Model.Queries;
using Xunit;

namespace SourceCodeApi.Tests.Unit;

public class GetLatestContributorsQueryHandlerTests
{
    private readonly Mock<ISourceCodeClient> _sourceCodeClient;
    private readonly Mock<ILogger<GetLatestContributorsQueryHandler>> _logger;     
    private GetLatestContributorsQueryHandler _sut;

    public GetLatestContributorsQueryHandlerTests()
    {
        _sourceCodeClient = new Mock<ISourceCodeClient>();
        _logger = new Mock<ILogger<GetLatestContributorsQueryHandler>>();
        
        _sut = new GetLatestContributorsQueryHandler(_sourceCodeClient.Object, _logger.Object);
    }

    [Fact]
    public async Task WhenNoCommits_ShouldReturnEmptyList()
    {
        _sourceCodeClient.Setup(
            x => x.GetAllCommits(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SourceCodeCommit>());

        var result = await _sut.Handle(new GetLatestContributorsQuery
        {
            Repository = It.IsAny<string>(),
            RepositoryOwner = It.IsAny<string>(),
            MaxResult = 30
        }, CancellationToken.None);

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task When50Commits_ShouldTakeTheLatest30()
    {
        _sourceCodeClient.Setup(
                x => x.GetAllCommits(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetTestCommits(50, DateTime.Parse("2022-09-01T12:00:00")));

        var result = await _sut.Handle(new GetLatestContributorsQuery
        {
            Repository = "ebxTest",
            RepositoryOwner = "ebxUser",
            MaxResult = 30
        }, CancellationToken.None);

        result.Should().NotBeEmpty();
        result.Count.Should().Be(30);

        result.Where(x => x.CommitTimeStamp < DateTime.Parse("2022-09-20")).Should().BeEmpty();
    }

    [Fact]
    public async Task WhenSourceCodeClientThrowsExcpetion_ShouldThrowAnException()
    {
        _sourceCodeClient.Setup(
                x => x.GetAllCommits(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ApplicationException());

        await _sut.Invoking(x => x.Handle(new GetLatestContributorsQuery
        {
            Repository = "ebxTest",
            RepositoryOwner = "ebxUser",
            MaxResult = 30
        }, CancellationToken.None)).Should().ThrowAsync<ApplicationException>();
    }

    private static IEnumerable<SourceCodeCommit> GetTestCommits(int number, DateTime initialCommitDate)
    {
        var result = new List<SourceCodeCommit>();
        var counter = 0;
        var nextCommitDate = initialCommitDate;

        do
        {
            result.Add(new SourceCodeCommit
            {
                AuthorName = "testAuthor",
                Id = $"Commit_{counter}",
                TimeStamp = nextCommitDate
            });

            nextCommitDate = nextCommitDate.AddDays(1);

            counter += 1;

        } while (counter < number);

        return result;
    }
}