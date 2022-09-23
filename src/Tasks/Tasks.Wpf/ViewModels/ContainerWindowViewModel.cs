using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Wpf.Pages;

namespace Tasks.Wpf.ViewModels;

public class ContainerWindowViewModel
{
    public AccountPage AccountPage { get; set; }
    public EventsPage EventsPage { get; set; }
    public HomePage HomePage { get; set; }
    public LabelsPage LabelsPage { get; set; }
    public RecurrencesPage RecurrencesPage { get; set; }

    public ContainerWindowViewModel(AccountPage accountPage, EventsPage eventsPage, HomePage homePage, LabelsPage labelsPage, RecurrencesPage recurrencesPage)
    {
        AccountPage = accountPage;
        EventsPage = eventsPage;
        HomePage = homePage;
        LabelsPage = labelsPage;
        RecurrencesPage = recurrencesPage;
    }


}
