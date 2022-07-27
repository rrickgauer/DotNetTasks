
// imports
import { ApiRecurrences } from "../../api/api-recurrences";
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

    getWeeklyRecurrences();
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

async function getWeeklyRecurrences() {
    const dateVal = recurrencesBoardController.getDateValue();
    const api = new ApiRecurrences();
    const response = await api.get(dateVal);
    
    const recurrencesHtml = await response.text();
    
    $('#recurrences-board-spinner').addClass('d-none');
    $('#recurrences-board-container').html(recurrencesHtml);
}

