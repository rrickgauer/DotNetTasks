
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

    /**
     * Submit the event form.
     */
    _submitForm = async () => {
        const model = this._getEventModelFromFormValues(Utililties.getNewUUID());

        // send request
        const api = new ApiEvents();
        const response = await api.put(model);

        if (response.ok) {
            EventModalActions.hideModal();
            EventModalActions.resetForm();

            const responseBody = await response.json();
        }
        else {
            console.error(await response.text());
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

    
    /**
     * View an event in the modal
     * @param {string} eventId - the event id to view
     */
    viewEvent = (eventId) => {
        EventModalActions.setModalEventIdAttribute(eventId);
        EventModalActions.showModal();
    }

    /**
     * Create a new event in the modal.
     */
    createNewEvent = () => {
        const newEventId = Utililties.getNewUUID();
        EventModalActions.setModalEventIdAttribute(newEventId);
        EventModalActions.showModal();
    }
}

