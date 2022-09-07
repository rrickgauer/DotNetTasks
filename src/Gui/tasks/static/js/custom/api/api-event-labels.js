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
}