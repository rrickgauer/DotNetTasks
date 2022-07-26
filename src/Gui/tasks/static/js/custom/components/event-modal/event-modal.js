
import { EventModalForm } from "./event-modal-form";
import { EventMapper } from "../../mappers/event-mappers";
import { ApiEvents } from "../../api/api-events";
import { Utililties } from "../../helpers/utilities";
import { Event } from "../../domain/models/event";
import { EventModalActions } from "./actions";

export class EventModal
{
    constructor() {
        this.eventModalForm = new EventModalForm();
    }

    //#region Form submissions

    /**
     * Add a submission event listener to the event form.
     */
    listenForEventFormSubmissions = () => {
        $(this.eventModalForm.form).on('submit', (submissionEvent) => {
            submissionEvent.preventDefault();
            this._submitForm();
        });
    }

    _submitForm = async () => {
        const model = this._getEventModelFromFormValues(Utililties.getNewUUID());

        console.log(model);
        return;

        // send request
        const api = new ApiEvents();
        const response = await api.put(model);

        if (response.ok) {
            const responseBody = await response.json();
            console.log(responseBody);
        }
    }
    
    
    /**
     * Build an Event domain model from the form's current values
     * @param {string} eventId the event's id
     * @returns {Event}
     */
    _getEventModelFromFormValues = (eventId) => {
        const formValues = this.eventModalForm.getValues();

        // map the form values to an Event modal
        const model = EventMapper.ToModelFromFormValues(formValues);
        
        model.id = eventId;

        return model;
    }

    //#endregion

    
    viewEvent = (eventId) => {
        EventModalActions.setModalEventIdAttribute(eventId);
        EventModalActions.showModal();
    }

    createNewEvent = () => {
        const newEventId = Utililties.getNewUUID();
        EventModalActions.setModalEventIdAttribute(newEventId);
        EventModalActions.showModal();
    }
}


