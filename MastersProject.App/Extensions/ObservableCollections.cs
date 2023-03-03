using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MastersProject.App.Extensions;

public static class ObservableCollections
{
    public static ReadOnlyObservableCollection<T> ToReadonlyObservable<T>(this ObservableCollection<T> collection)
    {
        return new ReadOnlyObservableCollection<T>(collection);
    }
    public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection)
    {
        return new ObservableCollection<T>(collection);
    }

    public static ReadOnlyObservableCollection<T> ToReadonlyObservable<T>(this IEnumerable<T> collection)
    {
        return new ReadOnlyObservableCollection<T>(collection.ToObservable());
    }
}