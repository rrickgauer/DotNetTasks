using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations;

public class EventLabelServices : IEventLabelServices
{
    private readonly IEventLabelRepository _eventLabelRepository;


    public EventLabelServices(IEventLabelRepository eventLabelRepository)
    {
        _eventLabelRepository = eventLabelRepository;
    }
}
