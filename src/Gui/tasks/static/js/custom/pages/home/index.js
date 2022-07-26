
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

    recurrencesBoardController.addListeners();

    // listen for event modal form submission
    eventModal.listenForEventFormSubmissions();
}

