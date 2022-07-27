// import { DateTime } from "../../../lib/luxon";
import { DateTime } from "../../lib/luxon";

export class DateTimeUtil
{

    /**
     * Get the current date formatted in an ISO string: 1999-01-01
     * @returns {String}
     */
    static getCurrentDateIso = () => DateTimeUtil.getCurrentDatetime().toISODate();

    /**
     * Get the current datetime
     * @returns {DateTime}
     */
    static getCurrentDatetime = () => DateTime.now();

    
    /**
     * Parse the specified datetime string into a Luxon DateTime object
     * @param {string} val datetime value string
     * @returns {DateTime}
     */
    static toDateTime = (val) => DateTime.fromISO(val);

}