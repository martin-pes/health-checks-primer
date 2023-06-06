using Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;

namespace HealthChecksPrimer.Common;

public class StorageBlob : IStorage
{
    public StorageBlob(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task SaveAsync(DateTimeOffset dateTime, string symbol, decimal price)
    {
        var json = new { DateTime = dateTime, Symbol = symbol, Price = price };
        var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(json)));
        ms.Seek(0, SeekOrigin.Begin);

        var containerClient = _blobServiceClient.GetBlobContainerClient("stocks");
        var blobClient = containerClient.GetBlobClient($"{symbol}-{Guid.NewGuid()}.json");
        await blobClient.UploadAsync(ms, overwrite: true);
    }

    private readonly BlobServiceClient _blobServiceClient;
}
