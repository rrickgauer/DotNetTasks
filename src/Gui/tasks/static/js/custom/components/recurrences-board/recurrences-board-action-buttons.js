
export class RecurrencesBoardActionButtons
{
    constructor() {

        /** @type {HTMLButtonElement} */
        this.newButton   = document.getElementById(RecurrencesBoardActionButtons.NEW);

        /** @type {HTMLInputElement} */
        this.dateInput   = document.getElementById(RecurrencesBoardActionButtons.DATE);

        /** @type {HTMLButtonElement} */
        this.todayButton = document.getElementById(RecurrencesBoardActionButtons.TODAY);

        /** @type {HTMLAnchorElement} */
        this.previousButton = document.getElementById(RecurrencesBoardActionButtons.PREVIOUS_WEEK);

        /** @type {HTMLAnchorElement} */
        this.nextButton = document.getElementById(RecurrencesBoardActionButtons.NEXT_WEEK);

        /** @type {HTMLDivElement} */
        this.spinner = document.getElementById(RecurrencesBoardActionButtons.SPINNER);

        /** @type {HTMLDivElement} */
        this.container = document.getElementById(RecurrencesBoardActionButtons.CONTAINER);
    }
}

RecurrencesBoardActionButtons.NEW           = 'recurrences-board-action-buttons-btn-new';
RecurrencesBoardActionButtons.DATE          = 'recurrences-board-action-buttons-input-date';
RecurrencesBoardActionButtons.TODAY         = 'recurrences-board-action-buttons-btn-today';
RecurrencesBoardActionButtons.PREVIOUS_WEEK = 'recurrences-board-action-buttons-btn-previous';
RecurrencesBoardActionButtons.NEXT_WEEK     = 'recurrences-board-action-buttons-btn-next';
RecurrencesBoardActionButtons.SPINNER       = 'recurrences-board-spinner';
RecurrencesBoardActionButtons.CONTAINER     = 'recurrences-board-container-wrapper';


