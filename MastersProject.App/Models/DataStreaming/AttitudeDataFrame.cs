using MastersProject.App.Infrastructure.DataStreaming;

namespace MastersProject.App.Models.DataStreaming;

public record AttitudeDataFrame : DataFrame
{
    public required int RawPitch { get; init; }
    public required int RawRoll { get; init; }
}