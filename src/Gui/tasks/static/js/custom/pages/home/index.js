//@ts-check

// imports
import { EventModal } from "../../components/event-modal/event-modal";
import { RecurrencesBoardActionsController } from "../../components/recurrences-board/recurrences-board-controller";
import { DailyRecurrenceListController } from "../../components/daily-recurrences-card/daily-recurrences-controller";
import { listenForArrowKeys } from "./page-listeners";
import { setupBoardActionVisibilities } from "./page-listeners";
import { listenForWindowResize } from "./page-listeners";
import { initCustomDatePickers } from "../../helpers/custom-datepicker";
import { DateTimeUtil } from "../../helpers/datetime";
import { DailyRecurrencesListItemController } from "../../components/daily-recurrences-list-item/daily-recurrences-list-item-controller";
import { AppSidebarController } from "../../components/app-sidebar/app-sidebar-controller";
import { EventLabelAssignmentsController } from "../../components/event-label-assignments-modal/controller";
import { AppSidebarLabelsFilterController } from "../../components/app-sidebar-labels-filter/controller";

// module variables
const m_eventModal = new EventModal();
const m_boardActionsController = new RecurrencesBoardActionsController();
const m_recurrenceListController = new DailyRecurrenceListController();
const m_listItemController = new DailyRecurrencesListItemController();
const m_sidebarController = new AppSidebarController();
const m_eventLabelModalController = new EventLabelAssignmentsController();


const m_sidebarLabelsController = new AppSidebarLabelsFilterController();

/**
 * Main logic
 */
$(document).ready(function() 
{
    initCustomDatePickers();
    addListeners();
    m_boardActionsController.getWeeklyRecurrences();
    setupBoardActionVisibilities();


    initLabelsSidebar();
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

    // board actions
    m_boardActionsController.addListeners();

    m_eventLabelModalController.addListeners();

    // event modal 
    m_eventModal.listenForFormSubmission();
    m_eventModal.listenForEventDeletion();
    m_eventModal.listenForFrequencyInputChange();
    m_eventModal.listenForDateInputChange();

    // daily recurrences list items
    m_listItemController.addEventListeners();

    // dailt recurrence cards
    m_recurrenceListController.listenForDailyRecurrencesCardNewEvent();

    m_sidebarController.addEventListeners();


    m_sidebarLabelsController.addListeners();

    listenForWindowResize();
    listenForArrowKeys();
}

function initLabelsSidebar()
{
    console.log(m_sidebarLabelsController);
}