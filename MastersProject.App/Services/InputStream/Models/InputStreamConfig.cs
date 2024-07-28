namespace MastersProject.App.Services.InputStream.Models;

public record InputStreamConfig
{
    public SerialConfig? Serial { get; init; } = null;
}