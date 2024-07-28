using MastersProject.App.Services.InputStream.Models;

namespace MastersProject.App.Services.InputStream;

public interface IInputStreamManager
{
    void Start();
    void Stop();
    void Pause();
    bool TryConfigure(InputStreamType type, InputStreamConfig config);
}