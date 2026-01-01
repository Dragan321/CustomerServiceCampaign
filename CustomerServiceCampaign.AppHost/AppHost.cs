var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CustomerServiceCampaign_Api>("customerservicecampaign-api");

builder.Build().Run();
