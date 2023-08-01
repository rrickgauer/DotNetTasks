import { ApiChecklists } from "../api/api-checklists";
import { Utililties } from "../helpers/utilities";
import { HttpRequestMapper } from "../mappers/http-request-mapper";



export class ChecklistServices
{

    constructor()
    {
        this.api = new ApiChecklists();
    }



    getAllChecklistHtml = async () =>
    {
        const response = await this.api.getAll();
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
        
        const response = await this.api.post(formData);
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

