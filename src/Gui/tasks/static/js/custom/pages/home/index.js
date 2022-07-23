
// imports
import { EventModal } from "../../components/event-modal/event-modal";

// module variables
const eventModal = new EventModal();


/**
 * Page entry point
 */
$(document).ready(function() {
    addListeners();
    
    
});


function addListeners() {
    $('.btn-create-new-event').on('click', function() {
        eventModal.createNewEvent();
    });


    eventModal.listenForEventFormSubmissions();
}



