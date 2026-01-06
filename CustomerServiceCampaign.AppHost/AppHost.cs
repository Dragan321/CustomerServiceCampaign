using CustomerServiceCampaign.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.CreateSqlServerDatabase();

var keycloak = builder.CreateKeycloakServer();


builder.AddProject<Projects.CustomerServiceCampaign_Api>("customerservicecampaign-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(keycloak)
    .WithExternalHttpEndpoints()
    .WithScalar();

builder.Build().Run();


