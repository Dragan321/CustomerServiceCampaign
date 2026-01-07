using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CustomerServiceCampaign.Modules.Campaigns.Application.Abstractions.Storage;

namespace CustomerServiceCampaign.Modules.Campaigns.Infrastructure.Storage;

internal sealed class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    private const string ContainerName = "purchases";

    public async Task<string> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

        string blobName = Guid.NewGuid().ToString();

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

        return blobName;
    }

    public async Task<Stream> DownloadAsync(string blobName, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        Response<BlobDownloadInfo> response = await blobClient.DownloadAsync(cancellationToken);

        return response.Value.Content;
    }

    public async Task DeleteAsync(string blobName, CancellationToken cancellationToken = default)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
    }
}
