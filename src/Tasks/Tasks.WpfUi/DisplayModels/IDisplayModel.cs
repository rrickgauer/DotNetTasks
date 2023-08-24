namespace Tasks.WpfUi.DisplayModels;

public interface IDisplayModel<T> where T: class, new()
{
    public abstract T Model { get; set; }
}
