
export class RecurrencesBoardActionButtons
{
    constructor() {

        /** @type {HTMLButtonElement} */
        this.newButton   = document.getElementById(RecurrencesBoardActionButtons.NEW);

        /** @type {HTMLInputElement} */
        this.dateInput   = document.getElementById(RecurrencesBoardActionButtons.DATE);

        /** @type {HTMLButtonElement} */
        this.todayButton = document.getElementById(RecurrencesBoardActionButtons.TODAY);

        /** @type {HTMLDivElement} */
        this.spinner = document.getElementById(RecurrencesBoardActionButtons.SPINNER);

        /** @type {HTMLDivElement} */
        this.container = document.getElementById(RecurrencesBoardActionButtons.CONTAINER);
    }
}

RecurrencesBoardActionButtons.NEW       = 'recurrences-board-action-buttons-btn-new';
RecurrencesBoardActionButtons.DATE      = 'recurrences-board-action-buttons-input-date';
RecurrencesBoardActionButtons.TODAY     = 'recurrences-board-action-buttons-btn-today';
RecurrencesBoardActionButtons.SPINNER   = 'recurrences-board-spinner';
RecurrencesBoardActionButtons.CONTAINER = 'recurrences-board-container-wrapper';

