namespace CustomerServiceCampaign.Common.Infrastructure.EventBus;

public sealed record RabbitMqSettings(string Host, string Username = "guest", string Password = "guest");
