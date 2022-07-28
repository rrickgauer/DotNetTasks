// imports
import { ApiRecurrences } from "../../api/api-recurrences";
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-actions-controller";
import {DailyRecurrenceListItemElements } from "../../components/daily-recurrences-card/daily-recurrences-list-elements";
import { RecurrencesListItemElement } from "../../components/daily-recurrences-card/list-item-element";


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

    listenForRecurrenceClick();

}

async function getWeeklyRecurrences() {
    const dateVal = boardActionsController.getDateValue();
    
    const api = new ApiRecurrences();
    const response = await api.get(dateVal);
    
    const recurrencesHtml = await response.text();
    boardActionsController.hideSpinner();
    boardActionsController.setBoardHtml(recurrencesHtml);
}


function listenForRecurrenceClick() {
    document.body.addEventListener('click', function(event) {
        if (event.target.classList.contains(DailyRecurrenceListItemElements.NAME)) {
            viewEvent(event.target);
        }
    });
}

/**
 * sd
 * @param {HTMLSpanElement} nameElement the name element
 */
function viewEvent(nameElement) {
    
    const listItem = new RecurrencesListItemElement();
    listItem.setListItemFromChildElement(nameElement);

    const eventId = listItem.getEventId();
    eventModal.viewEvent(eventId);

    // TODO: have the event modal fetch the data from the api and display them in the form inputs
}


