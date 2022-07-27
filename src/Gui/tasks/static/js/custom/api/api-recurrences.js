

import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
// import { Event } from "../domain/models/event";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { DateTime } from "../../lib/luxon";



export class ApiRecurrences
{

    /**
     * GET: /api/recurrences/:date
     * @param {DateTime} date recurrence date 
     * @returns {Promise<Response>}
     */
    get = async (date) => {
        const url = `${ApiEndpoints.RECURRENCES}/${date.toISODate()}`;
        return await fetch(url);
    }


}

