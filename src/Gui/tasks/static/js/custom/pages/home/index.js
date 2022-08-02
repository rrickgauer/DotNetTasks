// imports
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/controller";
import { EventModalSelectors } from "../../components/event-modal/event-modal-selectors";
import { KeyCodes } from "../../domain/constants/keycodes";
import { DailyRecurrenceListController } from "../../components/daily-recurrences-card/controller";

// module variables
const m_eventModal = new EventModal();
const m_boardActionsController = new RecurrencesBoardActionsController();
const m_listController = new DailyRecurrenceListController();

/**
 * Main logic
 */
$(document).ready(function() 
{
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

    m_eventModal.listenForEventModalFormSubmission();

    _deleteEventListener();

    window.addEventListener('resize', setupBoardActionVisibilities);    

    m_listController.listenForEventCompletions();
    m_listController.listenForRecurrenceClick();

    listenForArrowKeys();
}

/**
 * Listen for a delete event form submission
 */
async function _deleteEventListener() 
{
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
function setupBoardActionVisibilities() 
{
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
 * Listen for when users hit Ctrl+left/right/down
 * 
 * Ctrl + Left Arrow = jump to previous week
 * Ctrl + Right Arrow = jump to next week
 * Ctrl + Down Arrow = jump to current date
 */
function listenForArrowKeys() 
{
    
    document.addEventListener('keydown', function(e) 
    {
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

