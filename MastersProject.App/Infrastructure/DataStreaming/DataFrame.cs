namespace MastersProject.App.Infrastructure.DataStreaming;

public record DataFrame
{
    public required double Pitch { get; init; }

    public required double Roll { get; init; }

    public required int? Index { get; init; }

}