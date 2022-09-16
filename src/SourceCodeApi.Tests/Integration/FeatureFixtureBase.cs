using System.Net;
using FluentAssertions;
using LightBDD.XUnit2;
using Xunit;
using Xunit.Abstractions;

namespace SourceCodeApi.Tests.Integration;

public abstract class FeatureFixtureBase<TFunction> : FeatureFixture, IClassFixture<TFunction>, IAsyncLifetime where TFunction : IntegrationTestApp
{
    protected readonly HttpClient Client;
    protected HttpResponseMessage Response;
    
    protected FeatureFixtureBase(IntegrationTestApp app, ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        Client = app.CreateClient();
        Response = new HttpResponseMessage();
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync() => Task.CompletedTask;

    protected Task Should_return_status_code(HttpStatusCode statusCode)
    {
        Response.Should().NotBeNull();
        Response.StatusCode.Should().Be(statusCode);

        return Task.CompletedTask;
    }
}