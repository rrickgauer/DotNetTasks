﻿using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Responses;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;
using static Tasks.Domain.Responses.RepositoryResponses;
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
    /// Update the label with the specified id to the new values given in the UpdateLabelForm
    /// </summary>
    /// <param name="labelId"></param>
    /// <param name="userId"></param>
    /// <param name="updateLabelForm"></param>
    /// <returns></returns>
    public async Task<ModifyLabelResponse> UpdateLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm)
    {
        ModifyLabelResponse result = new()
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
    private bool UserCanUpdateLabel(DataRow? dataRow, Guid labelId, Guid userId, out Label? label)
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
        label = LabelMapper.ToModel(dataRow);

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

        result.Data = LabelMapper.ToModels(dataTable);

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
        Label label = LabelMapper.ToModel(selectResult.Data);

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
}
