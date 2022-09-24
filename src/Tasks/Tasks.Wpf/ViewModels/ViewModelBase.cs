using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tasks.CustomAttributes;
using Tasks.Mappers;

namespace Tasks.Wpf.ViewModels;

public class ViewModelBase : INotifyCollectionChanged
{
    /// <summary>
    /// Raise the property changed event for all the class's properties that have the RaisePropertyChange attribute
    /// </summary>
    protected void RaisePropertyChanges()
    {
        var properties = MapperUtilities.GetPropertiesWithAttribute(this.GetType(), typeof(RaisePropertyChangeAttribute));

        foreach (var property in properties)
        {
            RaisePropertyChanged(property.Name);
        }
    }

    /// <summary>
    /// This method raises PropertyChanged event 
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Invoke if there is any subscribers
    /// </summary>
    /// <param name="propertyName"></param>
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName=null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
    protected void SetPropertyChangedValue<T>(T? value, ref T? property) 
    {
        if (!EqualityComparer<T>.Default.Equals(property, value))
        {
            property = value;
            RaisePropertyChanges();
        }
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    protected void RaiseCollectionChanged(string propertyName)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, propertyName));
    }
}
