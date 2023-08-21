using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Responses.Custom;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Service.Services.Implementations;

public class ChecklistLabelServices : IChecklistLabelServices
{
    #region - Private Members -
    private readonly IChecklistLabelRepository _checklistLabelRepository;
    private readonly IMapperServices _mapperServices;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistLabelRepository"></param>
    /// <param name="mapperServices"></param>
    public ChecklistLabelServices(IChecklistLabelRepository checklistLabelRepository, IMapperServices mapperServices)
    {
        _checklistLabelRepository = checklistLabelRepository;
        _mapperServices = mapperServices;
    }

    /// <summary>
    /// Get all the labels that are assigned to the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IEnumerable<Label>> GetAssignedLabelsAsync(Guid checklistId)
    {
        var datatable = await _checklistLabelRepository.SelectAllLabelsAssignedToChecklistAsync(checklistId);

        var checklistLabelViews = _mapperServices.ToModels<ChecklistLabelView>(datatable);

        var labels = checklistLabelViews.Select(v => (Label)v);

        return labels;
    }

    /// <summary>
    /// Save the given ChecklistLabel
    /// </summary>
    /// <param name="checklistLabel"></param>
    /// <returns></returns>
    public async Task<int> SaveAsync(ChecklistLabel checklistLabel)
    {
        return await _checklistLabelRepository.ModifyAsync(checklistLabel);
    }

    /// <summary>
    /// Delete the checklist label assignment
    /// </summary>
    /// <param name="assignment"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(ChecklistLabelForm assignment)
    {
        return await _checklistLabelRepository.DeleteAsync(assignment);
    }

    public async Task<IEnumerable<ChecklistView>> GetAssignedChecklistsAsync(Guid labelId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ChecklistLabelView>> GetChecklistLabelsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LabelAssignment>> GetLabelAssignmentsForChecklist(Guid checklistId, Guid userId)
    {
        throw new NotImplementedException();
    }
}
