using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Domain.Models;


public interface ITableViewModel<TView, TModel> 
    where TModel : ITableViewModel<TView, TModel> 
    where TView : ITableView
{
    public static abstract explicit operator TModel(TView other);
}
