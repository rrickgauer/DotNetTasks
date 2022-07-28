//@ts-check

import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
// import { Utililties } from "../helpers/utilities";
import { Event } from "../domain/models/event";
import { HttpRequestMapper } from "../mappers/http-request-mapper";

export class ApiEvents
{
    /**
     * Send a put request.
     * @param {Event} eventModel the event object to send
     * @returns {Promise<Response>} the api response promise
     */
    put = async (eventModel) => {
        const formData = HttpRequestMapper.toFormData(eventModel);
        const url = `${ApiEndpoints.EVENTS}/${eventModel.id}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: formData,
        });
    }

    /**
     * Get the event from the api
     * @param {string} eventId the event id
     * @returns {Promise<Response>}
     */
    get = async (eventId) => {
        const url = `${ApiEndpoints.EVENTS}/${eventId}`;
        return await fetch(url);
    }


}

