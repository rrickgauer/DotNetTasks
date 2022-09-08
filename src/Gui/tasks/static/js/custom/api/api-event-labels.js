import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { Event } from "../domain/models/event";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { DateTime } from "../../lib/luxon";


export class ApiEventLabels
{
    /**
     * Get all
     * @param {string} eventId event id
     * @returns {Promise<Response>}
     */
    get = async (eventId) =>
    {
        const url = `${ApiEndpoints.EVENT_LABELS}/${eventId}/labels`;

        return await fetch(url);
    }

    /**
     * Make a batch update
     * @param {string} eventId event id
     * @param {Array<String>} labels label ids
     * @returns {Promise<Response>}
     */
    putBatch = async (eventId, labels) =>
    {
        const url = `${ApiEndpoints.EVENT_LABELS}/${eventId}/labels`;
        // const data = JSON.stringify(labels);

        const data = {labels: labels};

        return await fetch(url, {
            method: HttpMethods.PUT,
            // body: JSON.stringify(data),
            body: JSON.stringify(labels),
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
        })
    }
}