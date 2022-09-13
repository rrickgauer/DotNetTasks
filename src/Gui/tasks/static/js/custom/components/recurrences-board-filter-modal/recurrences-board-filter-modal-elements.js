


export class RecurrencesBoardFilterModalElements
{
    constructor()
    {
        /** @type {HTMLDivElement} */
        this.eModal = document.getElementById(RecurrencesBoardFilterModalElements.CONTAINER);

        /** @type {HTMLFormElement} */
        this.eForm = this.eModal.getElementsByClassName(RecurrencesBoardFilterModalElements.FORM)[0];
    }
}



RecurrencesBoardFilterModalElements.CONTAINER = 'recurrences-board-filter-modal';
RecurrencesBoardFilterModalElements.FORM = 'sidebar-container-labels-form';
