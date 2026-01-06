using System.Diagnostics;

namespace CustomerServiceCampaign.AppHost;

internal static class ResourceBuilderExtensions
{
    private static IResourceBuilder<T> WithOpenApiDocs<T>(this IResourceBuilder<T> resourceBuilder, string name,
        string displayName, string openApiPath)
        where T : IResourceWithEndpoints
    {
        return resourceBuilder.WithCommand(
            name,
            displayName,
            executeCommand: async _ =>
            {
                try
                {
                    var endpoint = resourceBuilder.GetEndpoint("http");
                    var url = $"{endpoint.Url}/{openApiPath}";

                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                    return new ExecuteCommandResult
                    {
                        Success = true
                    };
                }
                catch (Exception e)
                {
                    return new ExecuteCommandResult()
                    {
                        Success = false,
                        ErrorMessage = e.Message
                    };
                }
            }
        );
    }

    internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> resourceBuilder)
        where T : IResourceWithEndpoints
    {
        return resourceBuilder.WithOpenApiDocs(name: "scalar", displayName: "Scalar", openApiPath: "scalar/v1");
    }
    
    internal static IResourceBuilder<SqlServerDatabaseResource> CreateSqlServerDatabase(this IDistributedApplicationBuilder distributedApplicationBuilder)
    {
        var password = distributedApplicationBuilder.AddParameter("DatabasePassword", secret: true);

        var sql = distributedApplicationBuilder.AddSqlServer("mssql", password)
            .WithDataVolume()
            .WithLifetime(ContainerLifetime.Persistent);

        var resourceBuilder = sql.AddDatabase("customerservicecampaign-db");
        return resourceBuilder;
    }
    
    internal static IResourceBuilder<KeycloakResource> CreateKeycloakServer(this IDistributedApplicationBuilder distributedApplicationBuilder)
    {
        var password = distributedApplicationBuilder.AddParameter("KeycloakPassword", secret: true);
        var username = distributedApplicationBuilder.AddParameter("KeycloakAdmin", secret: true);
        
        var resourceBuilder = distributedApplicationBuilder.AddKeycloak("identity", 8080 , username, password)
            .WithDataVolume()
            .WithContainerName("identity")
            .WithLifetime(ContainerLifetime.Persistent)
            .WithExternalHttpEndpoints()
            .WithRealmImport("Resources/realm-export.json");

        return resourceBuilder;
    }
}