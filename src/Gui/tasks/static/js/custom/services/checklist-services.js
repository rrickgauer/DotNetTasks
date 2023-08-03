import { ApiChecklists } from "../api/api-checklists";
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

