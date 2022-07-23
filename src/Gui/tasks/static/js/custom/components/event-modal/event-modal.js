
import { EventModalForm } from "./form";
import { EventMapper } from "../../mappers/event-mappers";
import { ApiEvents } from "../../api/api-events";
import { Utililties } from "../../helpers/utilities";

export class EventModal
{
    constructor() {
        this.eventModalForm = new EventModalForm();
    }

    /**
     * Add a submission event listener to the event form.
     */
    listenForEventFormSubmissions = () => {
        $(this.eventModalForm.form).on('submit', (submissionEvent) => {
            submissionEvent.preventDefault();
            this.submitForm();
        });
    }

    submitForm = async () => {
        const model = this._getEventModelFromFormValues(Utililties.getNewUUID());

        // send request
        const api = new ApiEvents();
        const response = await api.put(model);

        if (response.ok) {
            const responseBody = await response.json();
            console.log(responseBody);
        }
    }

    _getEventModelFromFormValues = (eventId) => {
        const formValues = this.eventModalForm.getValues();

        // map the form values to an Event modal
        const model = EventMapper.ToModelFromFormValues(formValues);
        
        model.id = eventId;

        return model;
    }
}


