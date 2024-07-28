namespace MastersProject.App.Services.InputStream.Models;

public record SerialConfig
{
    public required string PortName { get; init; }

    public required int BaudRate { get; init; }
}