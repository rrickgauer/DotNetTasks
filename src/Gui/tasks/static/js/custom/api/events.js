//@ts-check

import { ApiEndpoints } from "./base";
import { HttpMethods } from "./base";
import { Event } from "../domain/models/event";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { DateTime } from "../../lib/luxon";

export class ApiEvents
{
    /**
     * PUT: /events/:eventId
     * @param {Event} eventModel the event object to send
     * @returns {Promise<Response>} the api response promise
     */
    put = async (eventModel) => 
    {
        const formData = HttpRequestMapper.toFormData(eventModel);
        const url = ApiEvents._getEventUrl(eventModel.id);

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: formData,
        });
    }

    /**
     * GET: /events/:eventId
     * @param {string} eventId the event id
     * @returns {Promise<Response>}
     */
    get = async (eventId) => 
    {
        const url = ApiEvents._getEventUrl(eventId);
        return await fetch(url);
    }

    /** 
     * DELETE: /events/:eventId
     * @param {string} eventId the event id
     * @returns {Promise<Response>}
     */
    delete = async(eventId) => 
    {
        const url = ApiEvents._getEventUrl(eventId);

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }

    /** 
     * DELETE: /events/:eventId/:occurenceDate
     * @param {string} eventId the event id
     * @param {DateTime} occurenceDate the date the event occurs on
     * @returns {Promise<Response>}
     */
    deleteThisEvent = async(eventId, occurenceDate) => 
    {
        const url = ApiEvents._getEventUrl(eventId) + `/${occurenceDate.toISODate()}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }

    /** 
     * DELETE: /events/:eventId/:occurenceDate/remaining
     * @param {string} eventId the event id
     * @param {DateTime} occurenceDate the date the event occurs on
     * @returns {Promise<Response>}
     */
    deleteThisEventAndFollowing = async(eventId, occurenceDate) => 
    {
        const url = ApiEvents._getEventUrl(eventId) + `/${occurenceDate.toISODate()}/remaining`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }

    static _getEventUrl = (eventId) => `${ApiEndpoints.EVENTS}/${eventId}`;
}

