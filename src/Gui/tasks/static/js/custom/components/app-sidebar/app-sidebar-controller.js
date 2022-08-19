import { AppSidebarElements } from "./app-sidebar-elements"


export class AppSidebarController
{
    constructor()
    {
        /** @type {AppSidebarElements} */
        this.elements = new AppSidebarElements();

        /** @type {HTMLDivElement} */
        this.eRecurrencesBoardWrapper = document.getElementById('recurrences-board-container-wrapper');
    }

    /**
     * Add the event listeners to the page
     */
    addEventListeners = () =>
    {
        this._listenForToggleCompletedEventsBtnClick();
    }


    //#region Toggle completed events

    /**
     * Listen for the toggle completed events button click
     */
    _listenForToggleCompletedEventsBtnClick = () =>
    {
        this.elements.eToggleCompletedEventsBtn.addEventListener('click', (e) => {
            this._toggleCompletedEvents();
        });
    }

    /**
     * Toggle the completed events visibility
     */
    _toggleCompletedEvents = () =>
    {
        this.eRecurrencesBoardWrapper.classList.toggle(AppSidebarController.TOGGLE_COMPLETED_EVENTS_CLASS);
    }

    //#endregion

}


AppSidebarController.TOGGLE_COMPLETED_EVENTS_CLASS = 'hide-completed';