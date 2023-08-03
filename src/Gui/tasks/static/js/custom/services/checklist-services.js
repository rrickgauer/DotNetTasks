import { ApiChecklists } from "../api/api-checklists";
import { UpdateChecklistForm } from "../domain/forms/update-checklist-form";
import { HttpRequestMapper } from "../mappers/http-request-mapper";



export class ChecklistServices
{
    #api = new ApiChecklists();

    
    getAllChecklistHtml = async () =>
    {
        const response = await this.#api.getAll();
        this.#handleBadResponse(response);

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
        this.#handleBadResponse(response);
        
        return await response.json();
    }

    getChecklistHtml = async (checklistId) =>
    {
        const response = await this.#api.get(checklistId);
        this.#handleBadResponse(response);

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

        this.#handleBadResponse(response);

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
        this.#handleBadResponse(response);
        
        return await response.json();
    }



    /**
     * Handle a bad response
     * @param {Response} response The response to handle
     */
    #handleBadResponse = async (response) =>
    {
        if (!response.ok)
        {
            const text = await response.text();
            throw new Error(text);
        }
    }



}

