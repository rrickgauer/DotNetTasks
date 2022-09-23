using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Services.Interfaces;
using Tasks.Wpf.Services;

namespace Tasks.Wpf.ViewModels;

public class EventsPageViewModel
{
    public IEventServices EventServices { get; set; }
    public WpfApplicationServices ApplicationServices { get; set; }

    public EventsPageViewModel(IEventServices eventServices, WpfApplicationServices applicationServices)
    {
        EventServices = eventServices;
        ApplicationServices = applicationServices;
    }
}
