using System;

namespace MastersProject.App.Services.AttitudeDataStream;

public interface IAttitudeStreamReader
{
    event EventHandler<AttitudeDataFrame> AttitudeDataPublishedEvent;
}