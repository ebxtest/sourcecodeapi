using FluentAssertions;
using Newtonsoft.Json;
using SourceCodeApi.Model;
using Xunit.Abstractions;

namespace SourceCodeApi.Tests.Integration.Features;

public partial class GetLatestCommitsTests: FeatureFixtureBase<IntegrationTestApp>
{
    public GetLatestCommitsTests(IntegrationTestApp app, ITestOutputHelper testOutputHelper) : base(app, testOutputHelper)
    {
    }

    private async Task When_get_latest_commits_for_a_repository(string ebxtest, string repo)
    {
        Response = await Client.GetAsync($"/api/v1/{ebxtest}/{repo}/contributors"); ;
    }

    private async Task And_should_return_commits()
    {
        Response.Should().NotBeNull();
        Response.Content.Should().NotBeNull();

        var contentJsonString = await Response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<ContributorCommit>>(contentJsonString);

        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
    }
}