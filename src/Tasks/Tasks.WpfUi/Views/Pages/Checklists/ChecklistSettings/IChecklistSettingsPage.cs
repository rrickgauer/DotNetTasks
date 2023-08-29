using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

public interface IChecklistSettingsPage<out T> : INavigableView<T> where T : class, IChecklistSettings
{

}
