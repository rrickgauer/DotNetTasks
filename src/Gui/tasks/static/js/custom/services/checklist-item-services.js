import { ApiChecklistItemComplete } from "../api/api-checklist-item-complete";
import { ApiChecklistItems } from "../api/api-checklist-items";
import { CreateChecklistItemForm } from "../domain/forms/checklist-item-forms";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { ServiceUtilities } from "./service-utilities";




export class ChecklistItemServices
{
    constructor(checklistId)
    {
        /** @type {string} */
        this.checklistId = checklistId;

        this.apiChecklistItems = new ApiChecklistItems(this.checklistId);
    }


    getChecklistItemsHtml = async () =>
    {
        const response = await this.apiChecklistItems.getAll();
        await ServiceUtilities.handleBadResponse(response);
        return response.text();
    }


    markItemComplete = async (checklistItemId) =>
    {
        const api = new ApiChecklistItemComplete(this.checklistId, checklistItemId);

        const response = await api.put();
        await ServiceUtilities.handleBadResponse(response);
        return response.ok;
    }

    markItemIncomplete = async (checklistItemId) =>
    {
        const api = new ApiChecklistItemComplete(this.checklistId, checklistItemId);

        const response = await api.delete();
        await ServiceUtilities.handleBadResponse(response);
        return response.ok;
    }


    /**
     * Create a new checklist item
     * @param {CreateChecklistItemForm} newItemForm
     */
    createNewItem = async (newItemForm) =>
    {
        const formData = HttpRequestMapper.toFormData(newItemForm);
        const response = await this.apiChecklistItems.post(formData);
        await ServiceUtilities.handleBadResponse(response);
        return await response.text();
    }
}