namespace MastersProject.App.Services.AttitudeDataStream;

public interface IAttitudeDataStreamWriter
{
    void WriteAttitude(double pitch, double roll);
}