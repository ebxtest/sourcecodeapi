using System.Net;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

namespace SourceCodeApi.Tests.Integration.Features;

public partial class GetLatestCommitsTests
{
    [Scenario]
    public async Task GivenCommitsForARepo_ShouldReturnLatestCommits()
    {
        await Runner.RunScenarioAsync(
            _ => When_get_latest_commits_for_a_repository("ebxtest", "sourcecodeapi"),
            _ => Should_return_status_code(HttpStatusCode.OK),
            _ => And_should_return_commits()
        );
    }
}