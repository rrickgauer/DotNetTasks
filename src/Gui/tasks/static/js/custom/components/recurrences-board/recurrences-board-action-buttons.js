
export class RecurrencesBoardActionButtons
{
    constructor() {

        /** @type {HTMLButtonElement} */
        this.newButton   = document.getElementById(RecurrencesBoardActionButtons.NEW);

        /** @type {HTMLInputElement} */
        this.dateInput   = document.getElementById(RecurrencesBoardActionButtons.DATE);

        /** @type {HTMLButtonElement} */
        this.todayButton = document.getElementById(RecurrencesBoardActionButtons.TODAY);
    }
}

RecurrencesBoardActionButtons.NEW = 'recurrences-board-action-buttons-btn-new';
RecurrencesBoardActionButtons.DATE = 'recurrences-board-action-buttons-input-date';
RecurrencesBoardActionButtons.TODAY = 'recurrences-board-action-buttons-btn-today';
