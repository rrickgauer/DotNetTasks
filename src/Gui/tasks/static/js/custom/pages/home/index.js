// imports
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-controller";
import { DailyRecurrenceListController } from "../../components/daily-recurrences-card/daily-recurrences-controller";
import { listenForArrowKeys } from "./page-listeners";
import { setupBoardActionVisibilities } from "./page-listeners";
import { listenForWindowResize } from "./page-listeners";
import { initCustomDatePickers } from "../../helpers/custom-datepicker";
import { DateTimeUtil } from "../../helpers/datetime";

// module variables
const m_eventModal = new EventModal();
const m_boardActionsController = new RecurrencesBoardActionsController();
const m_listController = new DailyRecurrenceListController();

/**
 * Main logic
 */
$(document).ready(function() 
{
    initCustomDatePickers();
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
        const today = DateTimeUtil.getCurrentDatetime();
        m_eventModal.createNewEventStartsOn(today);
    });

    m_boardActionsController.addListeners();

    m_eventModal.listenForFormSubmission();
    m_eventModal.listenForEventDeletion();
    m_eventModal.listenForFrequencyInputChange();
    m_eventModal.listenForDateInputChange();

    m_listController.listenForEventCompletions();
    m_listController.listenForRecurrenceClick();
    m_listController.listenForDailyRecurrencesCardNewEvent();

    listenForWindowResize();
    listenForArrowKeys();

}

