
import { DateTime } from "../../../lib/luxon";
import { ApiCompletions } from "../../api/completion";
import { EventCompletion } from "../../domain/models/event-completion";
import { DateTimeUtil } from "../../helpers/datetime";
import { DailyRecurrenceListElements, DailyRecurrenceListItemElements } from "./elements";

export class RecurrencesListItemElement
{
    constructor(eListItem) {
        /** @type {HTMLDivElement} */
        this.eListItem = eListItem;

        /** @type {String} */
        this.eventId = this.getEventIdAttributeValue();

        /** @type {HTMLDivElement} */
        this.eDailyRecurrencesCardParent = this.eListItem.closest(`.${DailyRecurrenceListElements.CARD}`);

        /** @type {String} */
        this._dateString = this.getOccurenceDateAttributeValue();

        /** @type {DateTime} */
        this.occurenceDate = DateTimeUtil.toDateTime(this._dateString);

        /** @type {HTMLInputElement} */
        this.eCheckBox = this.eListItem.getElementsByClassName(DailyRecurrenceListItemElements.CHECK_BOX)[0];
    }

    //#region Getters
    getEventIdAttributeValue = () => this.eListItem.getAttribute(DailyRecurrenceListItemElements.Attributes.EVENT_ID);
    getOccurenceDateAttributeValue = () =>  this.eDailyRecurrencesCardParent.getAttribute(DailyRecurrenceListElements.Attributes.OCCURENCE_DATE);
    //#endregion


    /**
     * Toggle an event completion with the api
     */
    toggleEventCompletion = async () => {
        const isComplete = this.eCheckBox.checked;
        const model = this._toModel();
        const api = new ApiCompletions();

        // const response = await (isComplete ? api.put(model) : api.delete(model));
        const response = (isComplete ? api.put(model) : api.delete(model));

        this._toggleCompletedCss();

    }

    /**
     * Get an EventCompletion model from the current html element values
     * @returns {EventCompletion}
     */
    _toModel = () => {
        const model = new EventCompletion();
        model.onDate = this.occurenceDate.toISODate();
        model.eventId = this.eventId;

        return model;
    }

    /**
     * Toggle the completed CSS classes for the element
     */
    _toggleCompletedCss = () => {
        this.eListItem.classList.toggle(DailyRecurrenceListItemElements.COMPLETED);
    }




    //#region Static methods

    /**
     * Create a new RecurrencesListItemElement object from the specified child element.
     * @param {HTMLElement} eChild any child element in the list item container
     * @returns {RecurrencesListItemElement} the newly created object
     */
    static createFromChildElement = (eChild) => {
        const eListItem = eChild.closest(`.${DailyRecurrenceListItemElements.LIST_ITEM}`);
        return new RecurrencesListItemElement(eListItem);
    }

    //#endregion
}