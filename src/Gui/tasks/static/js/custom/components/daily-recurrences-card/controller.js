import { EventModal } from "../event-modal/event-modal";
import { DailyRecurrenceCard } from "./card-element";
import { DailyRecurrenceListItemElements } from "./elements";
import { RecurrencesListItemElement } from "./list-item";


export class DailyRecurrenceListController
{

    /** 
     * Constructor 
     */
    constructor() 
    {
        this.eventModal = new EventModal();
    }


    /**
     * Listen for an event completion action
     */
    listenForEventCompletions = () => 
    {
        document.body.addEventListener('change', (event) => 
        {
            if (!event.target.classList.contains(DailyRecurrenceListItemElements.CHECK_BOX)) 
                return;

            const listItem = RecurrencesListItemElement.createFromChildElement(event.target);
            listItem.toggleEventCompletion();
        });
    }


    /**
     * Listen for when a recurrence list item is clicked (opens the event modal)
     * View an event in the modal
     */
    listenForRecurrenceClick = () =>
    {
        document.body.addEventListener('click', (event) => 
        {
            if (event.target.classList.contains(DailyRecurrenceListItemElements.NAME)) 
            {
                const eCard = new DailyRecurrenceCard(event.target);
                const occurenceDate = eCard.occurenceDate;
                // this.eventModal.createNewEventStartsOn(occurenceDate);

                const listItem = RecurrencesListItemElement.createFromChildElement(event.target);
                this.eventModal.viewEvent(listItem.eventId, occurenceDate);
            }
        });
    }

    /**
     * Listen for the new event button click on the daily recurrence card.
     */
    listenForDailyRecurrencesCardNewEvent = () =>
    {
        document.body.addEventListener('click', (event) => 
        {
            const isNewEventButton = event.target.classList.contains('card-daily-recurrences-new-event-btn');
            if (!isNewEventButton)
            {
                return;
            }

            const eCard = new DailyRecurrenceCard(event.target);
            const occurenceDate = eCard.occurenceDate;
            this.eventModal.createNewEventStartsOn(occurenceDate);
        });

    }

}