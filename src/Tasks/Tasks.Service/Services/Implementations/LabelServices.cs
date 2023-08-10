using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Responses;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using static Tasks.Service.Domain.Responses.Basic.RepositoryResponses;
using static Tasks.Service.Domain.Responses.Basic.LabelServicesResponses;
using Tasks.Service.Mappers;

namespace Tasks.Service.Services.Implementations;


public class LabelServices : ILabelServices
{
    #region Private members
    private readonly ILabelRepository _labelRepository;
    private readonly IMapperServices _mapperServices;
    //private static LabelMapper _labelMapper = new();
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="labelRepository"></param>
    public LabelServices(ILabelRepository labelRepository, IMapperServices mapperServices)
    {
        _labelRepository = labelRepository;
        _mapperServices = mapperServices;
    }


    /// <summary>
    /// Update the label with the specified id to the new values given in the UpdateLabelForm
    /// </summary>
    /// <param name="labelId"></param>
    /// <param name="userId"></param>
    /// <param name="updateLabelForm"></param>
    /// <returns></returns>
    public async Task<SaveLabelResponse> SaveLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm)
    {
        SaveLabelResponse result = new()
        {
            Successful = true,
        };

        // verify that the user can update the label
        DataRow? dataRow = (await _labelRepository.SelectLabelAsync(labelId)).Data;

        if (!UserCanUpdateLabel(dataRow, labelId, userId, out Label? label))
        {
            result.Data = null;
            return result;
        }

        // copy over the existing (or new) label's data into the one we are going to send to the repo
        label.Name = updateLabelForm.Name;
        label.Color = updateLabelForm.Color;

        // have the repository update it in the database
        var repoResponse = await _labelRepository.ModifyLabelAsync(label);

        if (!repoResponse.Successful)
        {
            ResponseUtilities.TransferResponseData(repoResponse, result);
            return result;
        }

        result.Data = label;

        return result;
    }


    /// <summary>
    /// Checks if the user can update the given label
    /// </summary>
    /// <param name="dataRow"></param>
    /// <param name="labelId"></param>
    /// <param name="userId"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    private static bool UserCanUpdateLabel(DataRow? dataRow, Guid labelId, Guid userId, out Label? label)
    {
        // label DNE, so we just need to create a new one
        if (dataRow is null)
        {
            // we need to create a new label object
            label = new()
            {
                Id = labelId,
                UserId = userId,
                CreatedOn = DateTime.Now,
            };

            return true;
        }

        // label exists, so make sure the user owns it 
        var mapper = new LabelMapper();
        label = mapper.ToModel(dataRow);

        if (label.UserId != userId)
        {
            label = null;
            return false;
        }

        return true;
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

        result.Data = _mapperServices.ToModels<Label>(dataTable);

        return result;
    }

    /// <summary>
    /// Delete the label
    /// </summary>
    /// <param name="labelId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DeleteLabelResponse> DeleteLabelAsync(Guid labelId, Guid userId)
    {
        DeleteLabelResponse result = new()
        {
            Successful = true,
        };

        // verify that the label exists in the database
        var selectResult = await _labelRepository.SelectLabelAsync(labelId);

        if (!selectResult.Successful || selectResult.Data is null)
        {
            ResponseUtilities.TransferResponseData(selectResult, result);
            return result;
        }

        // make sure the user owns the label
        Label label = _mapperServices.ToModel<Label>(selectResult.Data);

        if (label.UserId != userId)
        {
            result.Data = null;
            return result;
        }

        // delete the label from the database
        ModifyResponse deleteResult = await _labelRepository.DeleteLabelAsync(label);

        if (!selectResult.Successful || selectResult.Data is null)
        {
            ResponseUtilities.TransferResponseData(selectResult, result);
            return result;
        }

        result.Data = deleteResult.Data;

        return result;
    }


    /// <summary>
    /// Checks if the specified user owns all the of given label ids.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="labelIds"></param>
    /// <returns></returns>
    public async Task<bool> ClientOwnsLabelsAsync(Guid userId, IEnumerable<Guid> labelIds)
    {
        // get all the user's current labels
        GetLabelsResponse getLabelsResponse = await GetLabelsAsync(userId);

        if (!getLabelsResponse.Successful && getLabelsResponse.Exception != null)
        {
            throw getLabelsResponse.Exception;
        }

        // extract the LabelId's from each of the user's Label objects
        var userLabelIds = from label in getLabelsResponse.Data select label.Id;

        // ensure that all of the specified label ids are within the user's label id list
        foreach(var labelId in labelIds)
        {
            if (!userLabelIds.Contains(labelId))
            {
                return false;
            }
        }

        return true;

    }
}
