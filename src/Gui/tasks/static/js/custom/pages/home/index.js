
// imports
import { ApiRecurrences } from "../../api/api-recurrences";
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-actions-controller";

// module variables
const eventModal = new EventModal();
const boardActionsController = new RecurrencesBoardActionsController();


/**
 * Main logic
 */
$(document).ready(function() {
    addListeners();
    getWeeklyRecurrences();
});

/**
 * Add the event listeners to the page.
 */
function addListeners() {
    // listen for "create new event" button click
    boardActionsController.actionButtons.newButton.addEventListener('click', function(e) {
        eventModal.createNewEvent();
    });

    boardActionsController.addListeners();

    // listen for event modal form submission
    eventModal.listenForEventFormSubmissions();
}

async function getWeeklyRecurrences() {
    const dateVal = boardActionsController.getDateValue();
    
    const api = new ApiRecurrences();
    const response = await api.get(dateVal);
    
    const recurrencesHtml = await response.text();
    boardActionsController.hideSpinner();
    boardActionsController.setBoardHtml(recurrencesHtml);
}

