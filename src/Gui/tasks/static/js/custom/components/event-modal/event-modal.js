
import { EventModalForm } from "./form";

export class EventModal
{
    constructor() {
        this.eventModalForm = new EventModalForm();
    }

    /**
     * Add a submission event listener to the event form.
     */
    listenForEventFormSubmissions = () => {
        $(this.eventModalForm.form).on('submit', function(submissionEvent) {
            submissionEvent.preventDefault();
            alert('submit event');
        });
    }
}


