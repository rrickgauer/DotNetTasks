
// imports
import { EventModal } from "../../components/event-modal/event-modal";

// module variables
const eventModal = new EventModal();


/**
 * Page entry point
 */
$(document).ready(function() {
    // eventModal.init();
    eventModal.listenForEventFormSubmissions();
});



