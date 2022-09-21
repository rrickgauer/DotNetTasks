using System.ComponentModel;
using Tasks.CustomAttributes;
using Tasks.Mappers;

namespace Tasks.Wpf.ViewModels;

public class ViewModelBase
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
    protected void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
