
import { ApiEndpoints } from "./api-base";
import { DateTime } from "../../lib/luxon";

export class ApiRecurrences
{
    /**
     * GET: /api/recurrences/:date
     * @param {DateTime} date recurrence date 
     * @returns {Promise<Response>} the response in a promise
     */
    get = async (date) => {
        const url = `${ApiEndpoints.RECURRENCES}/${date.toISODate()}`;
        return await fetch(url);
    }
}

