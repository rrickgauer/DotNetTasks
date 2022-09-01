using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations;


public class LabelRepository : ILabelRepository
{
    #region Private memebers
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public LabelRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new(_configs);
    }



}
