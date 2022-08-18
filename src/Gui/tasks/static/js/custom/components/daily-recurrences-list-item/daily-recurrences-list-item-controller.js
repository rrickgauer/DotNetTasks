
////@ts-check

import { ApiCompletions } from "../../api/api-completion";
import { DailyRecurrenceCard } from "../daily-recurrences-card/daily-recurrences-card-element";
import { EventModal } from "../event-modal/event-modal";
import { DailyRecurrencesListItemElement } from "./daily-recurrences-list-item-element";


export class DailyRecurrencesListItemController
{
    /**
     * Constructor
     */
    constructor()
    {
        this.eventModal = new EventModal();
    }


    /**
     * Add all the event listeners to the page
     */
    addEventListeners = () =>
    {
        this._listenForRecurrenceClick();
        this._listenForEventCompletions();
    }


    //#region Recurrence click

    /**
     * Listen for when a recurrence list item is clicked (opens the event modal)
     * View an event in the modal
     */
    _listenForRecurrenceClick = () => 
    {
        document.body.addEventListener('click', (event) => 
        {
            if (!event.target.classList.contains(DailyRecurrencesListItemElement.NAME)) 
            {
                return;
            }

            this._handleRecurrenceClick(event.target);
        });
    }


    /**
     * View an event in the modal
     * @param {HTMLDivElement} eventTarget the clicked child element
     */
    _handleRecurrenceClick = (eventTarget) => 
    {
        const eCard = new DailyRecurrenceCard(eventTarget);

        const listItem = this._getListItemFromChild(eventTarget);

        this.eventModal.viewEvent(listItem.eventId, eCard.occurenceDate);
    }

    //#endregion


    //#region Event completion

    /**
     * Listen for an event completion action
     */
    _listenForEventCompletions = () => 
    {
        document.body.addEventListener('change', (event) => 
        {
            if (!event.target.classList.contains(DailyRecurrencesListItemElement.CHECK_BOX))
            {
                return;
            }
                
            this._toggleEventCompletion(event.target);
        });
    }


    /**
     * Toggle an event completion with the api
     * @param {HTMLInputElement} eListItemCheckbox the checkbox that was clicked
     */
    _toggleEventCompletion = async (eListItemCheckbox) => 
    {
        const listItem = this._getListItemFromChild(eListItemCheckbox);

        const isComplete = listItem.eCheckBox.checked;
        const model = listItem.toModel();

        const api = new ApiCompletions();

        // const response = await (isComplete ? api.put(model) : api.delete(model));
        const response = (isComplete ? api.put(model) : api.delete(model));

        listItem.toggleCompletedCss();
    }


    //#endregion


    //#region Additional utilities

    /**
     * Get the parent daily-recurrences-list-item that contains the given child element
     * @param {HTMLElement} eChild a child element within a DailyRecurrencesListItemElement
     * @returns {DailyRecurrencesListItemElement}
     */
    _getListItemFromChild = (eChild) =>
    {
        const eListItem = eChild.closest(`.${DailyRecurrencesListItemElement.LIST_ITEM}`);

        return (eListItem == null ? null : new DailyRecurrencesListItemElement(eListItem));
    }

    //#endregion

}