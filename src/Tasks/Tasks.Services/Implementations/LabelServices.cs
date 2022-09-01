using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Responses.ServicesResponses;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;
using static Tasks.Domain.Responses.ServicesResponses.LabelServicesResponses;

namespace Tasks.Services.Implementations;


public class LabelServices : ILabelServices
{
    #region Private members
    private readonly ILabelRepository _labelRepository;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="labelRepository"></param>
    public LabelServices(ILabelRepository labelRepository)
    {
        _labelRepository = labelRepository;
    }

    public async Task<GetLabelsResponse> GetLabelsAsync(Guid userId)
    {
        GetLabelsResponse result = new()
        {
            Success = true,
        };

        

        try
        {



        }
        catch(Exception ex)
        {
            result.Exception = ex;
            result.Success = false;
        }


        




        return result;
    }
}
