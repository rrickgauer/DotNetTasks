// imports
import { ApiRecurrences } from "../../api/api-recurrences";
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-actions-controller";
import { DailyRecurrenceListItemElements } from "../../components/daily-recurrences-card/daily-recurrences-list-elements";
import { RecurrencesListItemElement } from "../../components/daily-recurrences-card/list-item-element";
import { EventModalSelectors } from "../../components/event-modal/event-modal-selectors";

// module variables
const eventModal = new EventModal();
const boardActionsController = new RecurrencesBoardActionsController();


/**
 * Main logic
 */
$(document).ready(function() {
    addListeners();
    getWeeklyRecurrences();
    setupBoardActionVisibilities();
});

/**
 * Add the event listeners to the page.
 */
async function addListeners() {
    // listen for "create new event" button click
    boardActionsController.actionButtons.newButton.addEventListener('click', function(e) {
        eventModal.createNewEvent();
    });

    boardActionsController.addListeners();

    // listen for event modal form submission    
    $(eventModal.eventModalForm.form).on('submit', (submissionEvent) => {
        submissionEvent.preventDefault();
        submitEventModalForm();
    });

    listenForRecurrenceClick();

    _deleteEventListener();

    window.addEventListener('resize', setupBoardActionVisibilities);
}


async function submitEventModalForm() {
    const result = await eventModal.submitForm();
    getWeeklyRecurrences();
}

async function getWeeklyRecurrences() {
    const dateVal = boardActionsController.getDateValue();
    
    const api = new ApiRecurrences();
    const response = await api.get(dateVal);
    
    const recurrencesHtml = await response.text();
    boardActionsController.hideSpinner();
    boardActionsController.setBoardHtml(recurrencesHtml);
}

//#region View an event in the modal

function listenForRecurrenceClick() {
    document.body.addEventListener('click', function(event) {
        if (event.target.classList.contains(DailyRecurrenceListItemElements.NAME)) {
            viewEvent(event.target);
        }
    });
}

/**
 * View an event in the modal
 * @param {HTMLSpanElement} nameElement the name element
 */
function viewEvent(nameElement) {
    const listItem = new RecurrencesListItemElement();
    listItem.setListItemFromChildElement(nameElement);

    const eventId = listItem.getEventId();
    eventModal.viewEvent(eventId);
}

//#endregion


/**
 * Listen for a delete event form submission
 */
async function _deleteEventListener() {
    const eDeletionForm = document.getElementById(EventModalSelectors.DeleteForm.FORM);

    eDeletionForm.addEventListener('submit', async function(event) {
        event.preventDefault();
        
        const eventDeleted = await eventModal.deleteEvent();

        if (eventDeleted) {
            getWeeklyRecurrences();
        }
        else {
            console.error('There was an error deleting the event');
        }
    });
}


/**
 * Toggle the action button collapse element
 */
function setupBoardActionVisibilities() {
    const eCollapseButton = document.getElementById('recurrences-board-action-buttons-collapse-btn');
    const eMenu = document.getElementById('recurrences-board-action-buttons-collapse');

    if (eCollapseButton.offsetParent == null) {
        eMenu.classList.add('show');
    }
    else {
        eMenu.classList.remove('show');
    }
}


