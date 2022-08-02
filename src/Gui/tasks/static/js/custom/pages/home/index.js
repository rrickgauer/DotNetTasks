// imports
import { ApiRecurrences } from "../../api/recurrences";
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-actions-controller";
import { DailyRecurrenceListItemElements } from "../../components/daily-recurrences-card/daily-recurrences-list-elements";
import { RecurrencesListItemElement } from "../../components/daily-recurrences-card/list-item-element";
import { EventModalSelectors } from "../../components/event-modal/event-modal-selectors";
import { KeyCodes } from "../../domain/constants/keycodes";

// module variables
const m_eventModal = new EventModal();
const m_boardActionsController = new RecurrencesBoardActionsController();





/**
 * Main logic
 */
$(document).ready(function() {
    addListeners();
    m_boardActionsController.getWeeklyRecurrences();
    setupBoardActionVisibilities();
});

/**
 * Add the event listeners to the page.
 */
async function addListeners() 
{
    // listen for "create new event" button click
    m_boardActionsController.actionButtons.newButton.addEventListener('click', function(e) {
        m_eventModal.createNewEvent();
    });

    m_boardActionsController.addListeners();

    listenForEventModalFormSubmission();

    // listen for when a recurrence list item is clicked (opens the event modal)
    listenForRecurrenceClick();

    _deleteEventListener();

    window.addEventListener('resize', setupBoardActionVisibilities);

    listenForEventCompletions();

    listenForArrowKeys();
}


/**
 * Listen for event modal form submissions
 */
function listenForEventModalFormSubmission() 
{
    m_eventModal.eventModalForm.form.addEventListener('submit', async (submissionEvent) => 
    {
        submissionEvent.preventDefault();

        await m_eventModal.submitForm();
        m_boardActionsController.getWeeklyRecurrences();

    });
}


/**
 * Listen for a delete event form submission
 */
async function _deleteEventListener() {
    const eDeletionForm = document.getElementById(EventModalSelectors.DeleteForm.FORM);

    eDeletionForm.addEventListener('submit', async function(event) {
        event.preventDefault();
        
        const eventDeleted = await m_eventModal.deleteEvent();

        if (eventDeleted) {
            m_boardActionsController.getWeeklyRecurrences();
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


/**
 * View an event in the modal
 */
function listenForRecurrenceClick() {
    document.body.addEventListener('click', function(event) {

        if (event.target.classList.contains(DailyRecurrenceListItemElements.NAME)) {
            const listItem = RecurrencesListItemElement.createFromChildElement(event.target);
            m_eventModal.viewEvent(listItem.eventId);
        }

    });
}

/**
 * Listen for an event completion action
 */
function listenForEventCompletions() {
    document.body.addEventListener('change', function(event) {
        if (!event.target.classList.contains(DailyRecurrenceListItemElements.CHECK_BOX)) {
            return;
        }

        const listItem = RecurrencesListItemElement.createFromChildElement(event.target);
        listItem.toggleEventCompletion();
    });
}


/**
 * Listen for when users hit Ctrl+left/right/down
 * 
 * Ctrl + Left Arrow = jump to previous week
 * Ctrl + Right Arrow = jump to next week
 * Ctrl + Down Arrow = jump to current date
 */
function listenForArrowKeys() {
    
    document.addEventListener('keydown', function(e) {
        if (!e.ctrlKey) 
        {
            return;
        }
        else if (e.shiftKey || e.altKey) 
        {
            return;
        }
        else if (e.target != document.body) 
        {
            return;
        }
        
    
        if (e.code == KeyCodes.ARROW_LEFT) 
        {
            m_boardActionsController.jumpToPreviousWeek();
        }
        else if (e.code == KeyCodes.ARROW_RIGHT) 
        {
            m_boardActionsController.jumpToNextWeek();
        }
        else if (e.code == KeyCodes.ARROW_UP) 
        {
            m_boardActionsController.jumpToCurrentDate();
        }
        else 
        {
            return;
        }

        console.log(e);

    });
}

