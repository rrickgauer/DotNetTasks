import { ApiChecklistClones } from "../api/api-checklist-clones";
import { ApiChecklists } from "../api/api-checklists";
import { CloneChecklistForm } from "../domain/forms/clone-checklist-form";
import { UpdateChecklistForm } from "../domain/forms/update-checklist-form";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { ServiceUtilities } from "./service-utilities";



export class ChecklistServices
{
    #api = new ApiChecklists();


    getAllChecklistHtml = async () =>
    {
        const response = await this.#api.getAll();
        await ServiceUtilities.handleBadResponse(response);

        return await response.text();
    }

    createNewChecklist = async (title) =>
    {
        const checklistData = {
            title: title,
            type: 'List',
        };

        const formData = HttpRequestMapper.toFormData(checklistData);
        
        const response = await this.#api.post(formData);
        await ServiceUtilities.handleBadResponse(response);
        
        return await response.json();
    }

    getChecklistHtml = async (checklistId) =>
    {
        const response = await this.#api.get(checklistId);
        await ServiceUtilities.handleBadResponse(response);

        return await response.text();
    }

    
    /**
     * Delete the specified checklist
     * @param {string} checklistId the checklist to delete
     * @returns if successful
     */
    deleteChecklist = async (checklistId) =>
    {
        const response = await this.#api.delete(checklistId);

        await ServiceUtilities.handleBadResponse(response);

        return response.ok;
    }


    /**
     * Save the checklist
     * @param {string} checklistId 
     * @param {UpdateChecklistForm} udpateChecklistForm 
     */
    saveChecklist = async (checklistId, udpateChecklistForm) =>
    {
        const formData = HttpRequestMapper.toFormData(udpateChecklistForm);
        
        const response = await this.#api.put(checklistId, formData);
        await ServiceUtilities.handleBadResponse(response);
        
        return await response.json();
    }


    /**
     * Clone the specified checklist
     * @param {string} checklistId the checklist to clone
     * @param {CloneChecklistForm} cloneChecklistForm the data required to clone a checklist
     */
    cloneChecklist = async (checklistId, cloneChecklistForm) =>
    {
        const apiChecklistClones = new ApiChecklistClones(checklistId);
        const formData = HttpRequestMapper.toFormData(cloneChecklistForm);
        
        // const response = await this.#api.put(checklistId, formData);
        const response = await apiChecklistClones.post(formData);
        await ServiceUtilities.handleBadResponse(response);
        
        return await response.json();
    }


}

