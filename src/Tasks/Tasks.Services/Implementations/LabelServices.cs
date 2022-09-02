using System.Data;
using Tasks.Domain.Responses;
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
    /// Get the specified label that belongs to the given user.
    /// </summary>
    /// <param name="labelId">Label id</param>
    /// <param name="userId">User id</param>
    /// <returns></returns>
    public async Task<GetLabelResponse> GetLabelAsync(Guid labelId, Guid userId)
    {
        GetLabelResponse result = new() { Successful = true };

        // get all the user's labels
        var getLabelsResponse = await GetLabelsAsync(userId);

        if (!getLabelsResponse.Successful || getLabelsResponse.Data is null)
        {
            ResponseUtilities.TransferResponseData(getLabelsResponse, result);
            return result;
        }
        
        // get the label out of the collection whose Id matches the given labelId
        // otherwise set the data to null
        result.Data = getLabelsResponse.Data.Where(l => l.Id == labelId).FirstOrDefault();

        return result;
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
        var repoResult = await _labelRepository.SelectLabelsAsync(userId);

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
