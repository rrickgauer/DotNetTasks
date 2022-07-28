
import { DailyRecurrenceListItemElements } from "./daily-recurrences-list-elements";

export class RecurrencesListItemElement
{
    constructor(eListItem) {
        
        /** @type {HTMLDivElement} */
        this.eListItem = eListItem;
    }

    /**
     * 
     * @param {HTMLElement} eChild a child element of the list item container
     */
    setListItemFromChildElement = (eChild) => {
        const parent = eChild.closest(`.${DailyRecurrenceListItemElements.LIST_ITEM}`);
        this.eListItem = parent;
    }

    getEventId = () => this.eListItem.getAttribute(DailyRecurrenceListItemElements.Attributes.EVENT_ID);
}