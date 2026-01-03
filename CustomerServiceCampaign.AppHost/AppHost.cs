using CustomerServiceCampaign.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CustomerServiceCampaign_Api>("customerservicecampaign-api")
    .WithExternalHttpEndpoints()
    .WithScalar();

builder.Build().Run();
