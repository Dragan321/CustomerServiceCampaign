namespace CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Storage;

public interface IBlobService
{
    Task<string> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);
    Task<Stream> DownloadAsync(string blobName, CancellationToken cancellationToken = default);
    Task DeleteAsync(string blobName, CancellationToken cancellationToken = default);
}
