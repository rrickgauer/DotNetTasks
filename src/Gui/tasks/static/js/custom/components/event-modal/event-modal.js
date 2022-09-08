//@ts-check

import { EventModalForm } from "./event-modal-form";
import { EventMapper } from "../../mappers/event-mappers";
import { ApiEvents } from "../../api/api-events";
import { Utililties } from "../../helpers/utilities";
import { Event } from "../../domain/models/event";
import { EventModalActions } from "./event-modal-actions";
import { SpinnerButton } from "../../helpers/spinner-button";
import { RecurrencesBoardActionsController } from "../recurrences-board/recurrences-board-controller";
import { DateTime } from "../../../lib/luxon";
import { EventModalInputToggle } from "./event-modal-input-toggle";
import { AlertPageTopSuccess } from "../page-alerts/alert-page-top";
import { DateTimeUtil } from "../../helpers/datetime";
import { DatePicker } from "../../helpers/custom-datepicker";
import { EventModalDeleteForm } from "./event-modal-delete-form";

export class EventModal 
{
    
    /**
     * Constructor for the event modal
     */
    constructor() 
    {
        this.eventModalForm               = new EventModalForm();
        this.boardActionsController       = new RecurrencesBoardActionsController();
        this.inputToggle                  = new EventModalInputToggle();
        this.deleteForm                   = new EventModalDeleteForm();
        this.deleteEventFormSpinnerButton = new SpinnerButton(this.deleteForm.eSubmitButton);
        this.apiEvents                    = new ApiEvents();
    }

    //#region Event listeners

    /**
     * Listen for event modal form submissions
     */
    listenForFormSubmission = async () =>
    {
        this.eventModalForm.form.addEventListener('submit', async (submissionEvent) => 
        {
            submissionEvent.preventDefault();

            const success = await this.submitForm();
            await this.boardActionsController.getWeeklyRecurrences();

            EventModalActions.hideModal();
            
            const alertTop = new AlertPageTopSuccess('Saved!');
            alertTop.show();
        });
    }


    /**
     * Listen for a delete event form submission
     */
    listenForEventDeletion = async ()  =>
    {
        this.deleteForm.eForm.addEventListener('submit', async (submissionEvent) => 
        {
            this._handleDeleteEventFormSubmission(submissionEvent);
        });
    }

    /**
     * Listen for the frequency input value to change
     */
    listenForFrequencyInputChange = () =>
    {
        this.eventModalForm.inputFrequency.addEventListener('change', (event) => 
        {
            this.inputToggle.toggleInputs();
        });
    }

    /**
     * Listen for starts on input value change
     */
    listenForDateInputChange = () =>
    {
        // set the minimum date value for the ends on input 
        this.eventModalForm.inputStartsOn.addEventListener('change', (event) => 
        {
            const startsOnValue = DateTimeUtil.toDateTime(this.eventModalForm.inputStartsOn.value);

            const flatpick = new DatePicker(this.eventModalForm.inputEndsOn);
            flatpick.setMinimumDate(startsOnValue);
        });
    }


    //#endregion

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
        const response = await this.apiEvents.put(model);

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

    //#region View event and get its data from api

    /**
     * View an event in the modal
     * @param {string} eventId the event id to view
     * @param {DateTime} occurenceDate the occurence date
     */
    viewEvent = async (eventId, occurenceDate) => {

        console.log('1-1');


        this._showLoadingSpinner();

        console.log('1-2');

        EventModalActions.resetForm();
        console.log('1-3');
        EventModalActions.setEventIdAttr(eventId);
        console.log('1-4');
        EventModalActions.setOccurenceDateAttr(occurenceDate);
        console.log('1-5');
        EventModalActions.showModal();
        console.log('1-6');
        
        const eventModel = await this._getEventData(eventId);
        console.log('1-7');
        
        console.log('\n\n');
        for(const key in eventModel)
        {
            console.log(`${key}: ${eventModel[key]}`);
        }
        console.log('\n\n');
        
        
        this.eventModalForm.setFormValues(eventModel);
        console.log('1-8');

        this._removeLoadingSpinner();
        console.log('1-9');
    }
    

    /**
     * Get the specified event from the api
     * @param {string} eventId the event id
     * @returns {Promise<Event>}
     */
    _getEventData = async (eventId) => 
    {
        const response = await this.apiEvents.get(eventId);

        if (!response.ok) {

            console.log('2-bad response');
            return null;
        }

        const responseData = await response.json();
        const mappedResult = EventMapper.ToModelFromApiGetRequest(JSON.parse(responseData));
        return mappedResult;
    }

    //#endregion

    //#region Create new event

    /**
     * Create a new event that starts on the specified date.
     * @param {DateTime} startsOn when the event starts on
     */
    createNewEventStartsOn = (startsOn) =>
    {
        this.createNewEvent();

        this.eventModalForm.setStartsOnValue(startsOn);
        this.eventModalForm.setEndsOnValue(startsOn);

        EventModalActions.setOccurenceDateAttr(startsOn);

        this.eventModalForm.inputSeparation.value = '1';
    }

    /**
     * Create a new event in the modal.
     */
    createNewEvent = () => 
    {
        this._showLoadingSpinner();
        
        const newEventId = Utililties.getNewUUID();
        EventModalActions.setEventIdAttr(newEventId);
        
        EventModalActions.resetForm();
        this.eventModalForm.fireInputChangeEvents();
        
        this._removeLoadingSpinner();
        
        EventModalActions.showModal();

        this.eventModalForm.inputName.focus();
    }

    //#endregion

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

    //#region Delete event

    /**
     * Steps to take when the delete event form is submitted
     * @param {SubmitEvent} submissionEvent the submission event
     */
    _handleDeleteEventFormSubmission = async (submissionEvent) =>
    {
        // stop the normal js processing
        submissionEvent.preventDefault();

        // disable the submit button and show the spinner
        this.deleteEventFormSpinnerButton.showSpinner();
            
        // send the delete request to the api
        const eventDeleted = await this._deleteEvent();

        // handle the api response
        if (eventDeleted)
        {
            this._handleSuccessfulDeleteRequest();
        }
        else
        {
            this._handleBadDeleteRequest();
        }
    }


    /**
     * Delete the current event.
     * @returns {Promise<Boolean>}
     */
    _deleteEvent = async () => {
        // get the required attribute values from the modal
        const eventId = this._getCurrentEventId();

        // await the api's response
        const response = await this.apiEvents.delete(eventId);

        // return if it was successful
        return response.ok;
    }


    /**
     * Steps to take when a delete request was unsuccessful
     */
    _handleBadDeleteRequest = () =>
    {
        console.error('There was an error deleting the event');
        alert('There was an error deleting the event');
    }

    /**
     * Steps to take after a successful DELETE event api request
     */
    _handleSuccessfulDeleteRequest = async () =>
    {
        // refresh the recurrences board
        await this.boardActionsController.getWeeklyRecurrences();

        // notify user that the event was successfully deleted
        const alertTop = new AlertPageTopSuccess('Event was deleted successfully.');
        alertTop.show();

        // close the modal and hide the delete form
        EventModalActions.hideModal();
        EventModalActions.hideDeleteForm();
        this.deleteEventFormSpinnerButton.reset();
    }


    //#endregion

    
    /**
     * Get the current event id
     * @returns {String}
     */
    _getCurrentEventId = () => EventModalActions.getEventIdAttr();
}


