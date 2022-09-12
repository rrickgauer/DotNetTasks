
import { ApiEndpoints } from "./api-base";
import { DateTime } from "../../lib/luxon";

export class ApiRecurrences
{


    /**
     * GET: /api/recurrences/:date
     * @param {DateTime} date the date object
     * @param {String} labels search parms
     * @returns {Promise<Response>}
     */
    get = async (date, labels=null) => 
    {
        const endpoint = `${ApiEndpoints.RECURRENCES}/${date.toISODate()}`;
        const url = new URL(endpoint, window.location.origin);

        if (labels != null)
        {
            url.searchParams.set('labels', labels);
        }

        console.log(url.toString());

        return await fetch(url);
        // return await fetch(path);
    }
}


