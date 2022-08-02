//@ts-check

import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { EventCompletion } from "../domain/models/event-completion";
import { DateTimeUtil } from "../helpers/datetime";

export class ApiCompletions
{
    /**
     * Send a put request.
     * @param {EventCompletion} completionModel the event object to send
     * @returns {Promise<Response>} the api response promise
     */
    put = async (completionModel) => {
        const url = ApiCompletions._getUrl(completionModel.eventId, completionModel.onDate);

        return await fetch(url, {
            method: HttpMethods.PUT,
        });
    }

    /**
     * Send a delete request.
     * @param {EventCompletion} completionModel the event object to send
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

