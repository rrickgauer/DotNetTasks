using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Mappers;
using System.Net;
using Tasks.Service.Errors;

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
    /// <exception cref="HttpResponseException"></exception>
    public async Task<Label?> SaveLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm)
    {
        // verify that the user can update the label
        var dataRow = await _labelRepository.SelectLabelAsync(labelId);

        if (!UserCanUpdateLabel(dataRow, labelId, userId, out Label? label))
        {
            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        if (label == null)
        {
            return null;
        }

        // copy over the existing (or new) label's data into the one we are going to send to the repo
        label.Name = updateLabelForm.Name;
        label.Color = updateLabelForm.Color;

        // have the repository update it in the database
        await _labelRepository.ModifyLabelAsync(label);

        return label;
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
    /// <param name="labelId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<Label?> GetLabelAsync(Guid labelId, Guid userId)
    {
        var labels = await GetLabelsAsync(userId);

        var result = labels.Where(l => l.Id == labelId).FirstOrDefault();

        return result;
    }



    /// <summary>
    /// Get a collection of all the specified user's labels
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Label>> GetLabelsAsync(Guid userId)
    {
        // fetch a DataTable from the repository
        var datatable = await _labelRepository.SelectLabelsAsync(userId);

        var labels =  _mapperServices.ToModels<Label>(datatable);

        return labels;
    }


    public async Task<int> DeleteLabelAsync(Guid labelId, Guid userId)
    {

        Label label = new()
        {
            Id = labelId,
            UserId = userId,
        };

        // delete the label from the database
        var numRecords = await _labelRepository.DeleteLabelAsync(label);

        return numRecords;
    }






    /// <summary>
    /// Checks if the specified user owns all the of given label ids.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="labelIds"></param>
    /// <returns></returns>
    public async Task<bool> ClientOwnsLabelsAsync(Guid userId, IEnumerable<Guid> labelIds)
    {
        var userLabelIds = await GetUserLabelIds(userId);

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

    /// <summary>
    /// Get all the ids for each label that is owned by the specified user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    private async Task<IEnumerable<Guid>> GetUserLabelIds(Guid userId)
    {
        var labels = await GetLabelsAsync(userId);

        // extract the LabelId's from each of the user's Label objects
        var labelIds = labels.Select(l => l.Id.Value);

        return labelIds;

    }
}
