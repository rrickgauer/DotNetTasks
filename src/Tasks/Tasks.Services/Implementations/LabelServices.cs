using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Responses;
using Tasks.Domain.Responses.ServicesResponses;
using Tasks.Mappers;
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

    /// <summary>
    /// Get a collection of all the specified user's labels
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<GetLabelsResponse> GetLabelsAsync(Guid userId)
    {
        GetLabelsResponse result = new()
        {
            Successful = true,
        };

        // fetch a DataTable from the repository
        var repoResult = await _labelRepository.SelectLabels(userId);

        if (!result.Successful)
        {
            ResponseUtilities.TransferResponseData(repoResult, result);
            return result;
        }


        // map out the records to models
        DataTable dataTable = repoResult.Data ?? (new());

        result.Data = LabelMapper.ToModels(dataTable);

        return result;
    }
}
