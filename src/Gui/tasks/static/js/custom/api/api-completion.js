//@ts-check

import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { EventCompletion } from "../domain/models/event-completion";

export class ApiCompletions
{
    /**
     * PUT: /completions/:eventId/:onDate
     * 
     * @param {EventCompletion} completionModel the event object to send
     * 
     * @returns {Promise<Response>} the api response promise
     */
    put = async (completionModel) => {
        const url = ApiCompletions._getUrl(completionModel.eventId, completionModel.onDate);

        return await fetch(url, {
            method: HttpMethods.PUT,
        });
    }

    /**
     * DELETE: /completions/:eventId/:onDate
     * 
     * @param {EventCompletion} completionModel the event object to send
     * 
     * @returns {Promise<Response>} the api response promise
     */
    delete = async (completionModel) => {
        const url = ApiCompletions._getUrl(completionModel.eventId, completionModel.onDate);

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }

    
    static _getUrl = (eventId, onDate) => `${ApiEndpoints.COMPLETIONS}/${eventId}/${onDate}`;
}

