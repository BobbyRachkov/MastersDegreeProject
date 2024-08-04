using System;

namespace MastersProject.App.Infrastructure.DataStreaming;

public interface IDataStreamReader<TData> where TData : DataFrame
{
    event EventHandler<TData> NewEntryPublished;
}