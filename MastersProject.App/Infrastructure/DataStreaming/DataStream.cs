using System;

namespace MastersProject.App.Infrastructure.DataStreaming;

public class DataStream<TData> : IDataStreamReader<TData>, IDataStreamWriter<TData> where TData : DataFrame
{
    public event EventHandler<TData>? NewEntryPublished;

    public virtual void WriteEntry(TData entry)
    {
        NewEntryPublished?.Invoke(this, entry);
    }

}