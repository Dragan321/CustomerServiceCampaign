using System.Reflection;
using CustomerServiceCampaign.Api.Extensions;
using CustomerServiceCampaign.Api.Middleware;
using CustomerServiceCampaign.Api.OpenApi;
using CustomerServiceCampaign.Api.OpenTelemetry;
using CustomerServiceCampaign.Common.Application;
using CustomerServiceCampaign.Common.Infrastructure;
using CustomerServiceCampaign.Common.Presentation.Endpoints;
using CustomerServiceCampaign.Modules.Users.Infrastructure;
using CustomerServiceCampaign.Modules.Campaigns.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();

builder.AddAzureBlobServiceClient("customerservicecampaign-storage");

Assembly[] moduleApplicationAssemblies = [
    CustomerServiceCampaign.Modules.Users.Application.AssemblyReference.Assembly,
    CustomerServiceCampaign.Modules.Campaigns.Application.AssemblyReference.Assembly
];

builder.Services.AddApplication(moduleApplicationAssemblies);

builder.Services.AddInfrastructure(DiagnosticsConfig.ServiceName);


builder.Configuration.AddModuleConfiguration(["users", "campaigns"]);

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddCampaignsModule(builder.Configuration);

builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
        
    app.ApplyMigrations();
}


app.UseLogContext();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.Run();