namespace MastersProject.App.Infrastructure.DataStreaming;

public interface IDataStreamWriter<in TData> where TData : DataFrame
{
    void WriteEntry(TData entry);
}