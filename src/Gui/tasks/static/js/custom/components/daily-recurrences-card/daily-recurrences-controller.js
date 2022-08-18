//// @ts-check
import { EventModal } from "../event-modal/event-modal";
import { DailyRecurrenceCard } from "./daily-recurrences-card-element";

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