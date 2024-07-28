using System;

namespace MastersProject.App.Services.AttitudeDataStream;

public class AttitudeDataStreamService : IAttitudeStreamReader, IAttitudeDataStreamWriter
{
    public event EventHandler<AttitudeDataFrame>? AttitudeDataPublishedEvent;

    public void WriteAttitude(double pitch, double roll)
    {
        AttitudeDataPublishedEvent?.Invoke(this, new AttitudeDataFrame
        {
            Pitch = pitch,
            Roll = roll
        });
    }
}