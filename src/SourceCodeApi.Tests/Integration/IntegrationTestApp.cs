using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Moq;

namespace SourceCodeApi.Tests.Integration;

public class IntegrationTestApp : WebApplicationFactory<Program>
{
    public IntegrationTestApp()
    {
        //Note we could mock the third party dependencies
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //Add mock for external dependencies if needed
        });
        return base.CreateHost(builder);
    }
}