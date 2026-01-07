using CustomerServiceCampaign.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.CreateSqlServerDatabase();

var keycloak = builder.CreateKeycloakServer();


var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator(azurite =>
    {
        azurite.WithDataVolume();
        azurite.WithLifetime(ContainerLifetime.Persistent);
    })
    .AddBlobs("customerservicecampaign-storage");

builder.AddProject<Projects.CustomerServiceCampaign_Api>("customerservicecampaign-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(storage)
    .WaitFor(storage)
    .WithReference(keycloak)
    .WithExternalHttpEndpoints()
    .WithScalar();

builder.Build().Run();


