
// imports
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardController } from "../../components/recurrences-board/recurrences-board-controller";

// module variables
const eventModal = new EventModal();
const recurrencesBoardController = new RecurrencesBoardController();


/**
 * Main logic
 */
$(document).ready(function() {
    addListeners();
});

/**
 * Add the event listeners to the page.
 */
function addListeners() {
    // listen for "create new event" button click
    recurrencesBoardController.actionButtons.newButton.addEventListener('click', function(e) {
        eventModal.createNewEvent();
    });

    // set the current recurrences date to today's value
    recurrencesBoardController.actionButtons.todayButton.addEventListener('click', function(e) {
        recurrencesBoardController.setDateValueToday();
    });

    // listen for event modal form submission
    eventModal.listenForEventFormSubmissions();
}

