
////@ts-check

import { ApiCompletions } from "../../api/api-completion";
import { DailyRecurrencesListItemDropdownBtnActions } from "../../domain/enums/daily-recurrences-list-item-dropdown-actions";
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
        this._listenForDropdownMenuBtnClick();
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


    //#region Dropdown menu buttons

    _listenForDropdownMenuBtnClick = () =>
    {
        document.body.addEventListener('click', (event) => 
        {
            if (!event.target.classList.contains(DailyRecurrencesListItemElement.DROPDOWN_BTN))
            {
                return;
            }

            this._handleDropdownMenuBtnClick(event.target);
        });
    }

    /**
     * Handle a dropdown menu button click event
     * @param {HTMLButtonElement} eClickedBtn the clicked button
     */
    _handleDropdownMenuBtnClick = (eClickedBtn) =>
    {
        const listItem = this._getListItemFromChild(eClickedBtn);
        const actionValue = this._getDropdownMenuBtnActionValue(eClickedBtn);

        switch(actionValue)
        {
            case DailyRecurrencesListItemDropdownBtnActions.DELETE_THIS_EVENT:
                alert('delete this event');
                break;

            case DailyRecurrencesListItemDropdownBtnActions.DELETE_THIS_AND_FOLLOWING_EVENTS:
                alert('delete this and following events');
                break;
        }
    }


    /**
     * Get the action data attribute value from the given dropdown menu button element
     * @param {HTMLButtonElement} eBtn the button element
     * @returns {Number}
     */
    _getDropdownMenuBtnActionValue = (eBtn) => 
    {
        const value = eBtn.getAttribute(DailyRecurrencesListItemElement.Attributes.DROPDOWN_BTN_ACTION);

        return parseInt(value);
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