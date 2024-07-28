namespace MastersProject.App.Services.AttitudeDataStream;

public record AttitudeDataFrame
{
    public required double Pitch { get; init; }
    public required double Roll { get; init; }
}