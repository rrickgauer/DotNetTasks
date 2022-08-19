
//// @ts-check

import { DateTime } from "../../../lib/luxon";
import { EventCompletion } from "../../domain/models/event-completion";
import { DateTimeUtil } from "../../helpers/datetime";
import { DailyRecurrenceListElements } from "../daily-recurrences-card/daily-recurrences-elements";

export class DailyRecurrencesListItemElement
{
    /**
     * Constructor
     * @param {Element} eListItem 
     */
    constructor(eListItem)
    {   
        /** @type {HTMLDivElement} */
        this.eListItem = eListItem;

        /** @type {HTMLDivElement} */
        this.eDailyRecurrencesCardParent = this.eListItem.closest(`.${DailyRecurrenceListElements.CARD}`);

        /** @type {HTMLInputElement} */
        this.eCheckBox = this.eListItem.getElementsByClassName(DailyRecurrencesListItemElement.CHECK_BOX).item(0);
        
        /** @type {String} */
        this.eventId = this.getEventIdAttributeValue();

        /** @type {String} */
        this._dateString = this.getOccurenceDateAttributeValue();

        /** @type {DateTime} */
        this.occurenceDate = DateTimeUtil.toDateTime(this._dateString);
    }


    /**
     * Get an EventCompletion model from the current html element values
     * @returns {EventCompletion}
     */
    toModel = () =>
    {
        const model = new EventCompletion();
        model.onDate = this.occurenceDate.toISODate();
        model.eventId = this.eventId;

        return model;
    }

    /**
     * Toggle the completed CSS classes for the element
     */
    toggleCompletedCss = () =>
    {
        this.eListItem.classList.toggle(DailyRecurrencesListItemElement.COMPLETED);
    }


    //#region Attribute value getters

    /**
     * Get the current event id attribute value
     * @returns {String}
     */
    getEventIdAttributeValue = () => 
    {
        return this.eListItem.getAttribute(DailyRecurrencesListItemElement.Attributes.EVENT_ID);
    }

    /**
     * Get the current occurence date attribute value
     * @returns {String}
     */
    getOccurenceDateAttributeValue = () =>  
    {
        return this.eDailyRecurrencesCardParent.getAttribute(DailyRecurrenceListElements.Attributes.OCCURENCE_DATE);
    }

    //#endregion

}



DailyRecurrencesListItemElement.LIST_ITEM    = 'daily-recurrences-list-item';
DailyRecurrencesListItemElement.CHECK_BOX    = 'daily-recurrences-list-item-checkbox';
DailyRecurrencesListItemElement.NAME         = 'daily-recurrences-list-item-name';
DailyRecurrencesListItemElement.COMPLETED    = 'completed';
DailyRecurrencesListItemElement.DROPDOWN     = 'daily-recurrences-list-item-dropdown';
DailyRecurrencesListItemElement.DROPDOWN_BTN = 'daily-recurrences-list-item-dropdown-btn';

DailyRecurrencesListItemElement.Attributes =  {
    EVENT_ID: 'data-js-event-id',
    DROPDOWN_BTN_ACTION: 'data-js-dropdown-action',
}