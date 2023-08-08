import { ApiChecklistItemComplete } from "../api/api-checklist-item-complete";
import { ApiChecklistItems } from "../api/api-checklist-items";
import { CreateChecklistItemForm, UpdateChecklistItemForm } from "../domain/forms/checklist-item-forms";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { ServiceUtilities } from "./service-utilities";


export class ChecklistItemServices
{
    /**
     * Checklist Item Services
     * @param {string} checklistId the parent checklist id
     */
    constructor(checklistId)
    {
        /** @type {string} */
        this.checklistId = checklistId;

        this.apiChecklistItems = new ApiChecklistItems(this.checklistId);
    }


    /**
     * Get the HTML from the api for an open checklist's items
     */
    getChecklistItemsHtml = async () =>
    {
        const response = await this.apiChecklistItems.getAll();
        
        await ServiceUtilities.handleBadResponse(response);
        
        return response.text();
    }


    /**
     * Mark the item as completed
     * @param {string} checklistItemId the checklist item's id
     * @returns the api response   
     */
    markItemComplete = async (checklistItemId) =>
    {
        const api = new ApiChecklistItemComplete(this.checklistId, checklistItemId);

        const response = await api.put();
        await ServiceUtilities.handleBadResponse(response);
        return response.ok;
    }

    
    /**
     * Mark the item as incomplete
     * @param {string} checklistItemId the checklist item's id
     * @returns if the request was successful
     */
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

    /**
     * Delete the specified checklist item
     * @param {string} checklistItemId the checklist item to delete
     */
    deleteChecklistItem = async (checklistItemId) =>
    {
        const response = await this.apiChecklistItems.delete(checklistItemId);
        await ServiceUtilities.handleBadResponse(response);
    }


    /**
     * Update the checklist item
     * @param {UpdateChecklistItemForm} updateChecklistItemForm 
     */
    updateChecklistItem = async (itemId, updateChecklistItemForm) =>
    {
        const form = HttpRequestMapper.toFormData(updateChecklistItemForm);
        const response = await this.apiChecklistItems.put(itemId, form);
        
        await ServiceUtilities.handleBadResponse(response);
        
        return await response.json();
    }
}