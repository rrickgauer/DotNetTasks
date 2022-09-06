using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations;

public class EventLabelRepository : IEventLabelRepository
{
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;

    public EventLabelRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new(_configs);
    }   
}
