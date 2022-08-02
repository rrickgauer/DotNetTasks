//@ts-check

import { EventModalForm } from "./event-modal-form";
import { EventMapper } from "../../mappers/event-mappers";
import { ApiEvents } from "../../api/events";
import { Utililties } from "../../helpers/utilities";
import { Event } from "../../domain/models/event";
import { EventModalActions } from "./actions";
import { EventModalSelectors } from "./event-modal-selectors";
import { SpinnerButton } from "../../helpers/spinner-button";
import { RecurrencesBoardActionsController } from "../recurrences-board/controller";

export class EventModal {
    constructor() {
        this.eventModalForm = new EventModalForm();
        this.boardActionsController = new RecurrencesBoardActionsController();
    }

    /**
     * Listen for event modal form submissions
     */
    listenForEventModalFormSubmission = () =>
    {
        this.eventModalForm.form.addEventListener('submit', async (submissionEvent) => 
        {
            submissionEvent.preventDefault();

            await this.submitForm();
            this.boardActionsController.getWeeklyRecurrences();
        });
    }




    //#region Form submissions

    /**
     * Submit the event form.
     */
    submitForm = async () => {
        const spinner = new SpinnerButton(this.eventModalForm.submitBtn);
        spinner.showSpinner();

        const eventId = EventModalActions.getEventIdAttr();
        const model = this._getEventModelFromFormValues(eventId);

        // send request
        const api = new ApiEvents();
        const response = await api.put(model);

        spinner.reset();

        return response.ok;
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
    viewEvent = async (eventId) => {
        this._showLoadingSpinner();

        EventModalActions.resetForm();

        EventModalActions.setEventIdAttr(eventId);
        EventModalActions.showModal();
        
        const eventModel = await this._getEventData(eventId);
        this.eventModalForm.setFormValues(eventModel);

        this._removeLoadingSpinner();
    }

    /**
     * Get the specified event from the api
     * @param {string} eventId the event id
     * @returns {Promise<Event>}
     */
    _getEventData = async (eventId) => {
        const api = new ApiEvents();
        const response = await api.get(eventId);

        if (!response.ok) {
            return null;
        }

        const responseData = await response.json();
        const mappedResult = EventMapper.ToModelFromApiGetRequest(JSON.parse(responseData));
        return mappedResult;
    }

    /**
     * Create a new event in the modal.
     */
    createNewEvent = () => {
        this._showLoadingSpinner();
        
        const newEventId = Utililties.getNewUUID();
        EventModalActions.setEventIdAttr(newEventId);
        
        EventModalActions.resetForm();
        
        this._removeLoadingSpinner();
        
        EventModalActions.showModal();
    }

    //#region Show/hide form and spinner

    /**
     * Show the spinner and hide the form
     */
    _showLoadingSpinner = () => {
        EventModalActions.showSpinner();
        EventModalActions.hideForm();
    }

    /**
     * Show the form, hide the spinner
     */
    _removeLoadingSpinner = () => {
        EventModalActions.hideSpinner();
        EventModalActions.showForm();
    }

    //#endregion


    /**
     * Delete the current event.
     * @returns {Promise<Boolean>}
     */
    deleteEvent = async () => {
        // setup a spinner button for the submit button
        const eSubmitButton = $(`#${EventModalSelectors.DeleteForm.SUBMIT_BTN}`);
        const spinner = new SpinnerButton(eSubmitButton);
        spinner.showSpinner();

        // send api request to delete the event
        const eventId = this._getCurrentEventId();
        const api = new ApiEvents();
        const response = await api.delete(eventId);
        const wasDeleted = response.ok;

        // close the modal and hide the delete form
        EventModalActions.hideModal();
        EventModalActions.hideDeleteForm();
        spinner.reset();

        return wasDeleted;
    }


    /**
     * Get the current event id
     * @returns {String}
     */
    _getCurrentEventId = () => EventModalActions.getEventIdAttr();
}


