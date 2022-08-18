import { DateTime } from "../../../lib/luxon";
import { DateTimeUtil } from "../../helpers/datetime";
import { DailyRecurrenceListElements } from "./daily-recurrences-elements";



export class DailyRecurrenceCard
{
    constructor(eChild)
    {
        /** @type {HTMLElement} */
        this.eChild = eChild;
        
        /** @type {HTMLDivElement} */
        this.eCard = this.eChild.closest(`.${DailyRecurrenceListElements.CARD}`);
        
        /** @type {HTMLDivElement} */
        this.eList = this.eCard.getElementsByClassName(DailyRecurrenceListElements.LIST)[0];

        /** @type {HTMLButtonElement} */
        this.eNewEventBtn = this.eCard.getElementsByClassName(DailyRecurrenceListElements.NEW_EVENT_BTN)[0];

        this.occurenceDateAttributeValue = this.eCard.getAttribute(DailyRecurrenceListElements.Attributes.OCCURENCE_DATE);
    }


    /**
     * @returns {DateTime}
     */
    get occurenceDate() { return DateTimeUtil.toDateTime(this.occurenceDateAttributeValue); }

}