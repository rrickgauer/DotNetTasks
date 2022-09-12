import { ApiEventLabels } from "../../api/api-event-labels";
import { Label } from "../../domain/models/label";
import { SpinnerButton } from "../../helpers/spinner-button";
import { AlertPageTopDanger, AlertPageTopSuccess } from "../page-alerts/alert-page-top";
import { EventLabelAssignmentsElements } from "./elements";


import { RecurrencesBoardActionsController } from "../recurrences-board/recurrences-board-controller";

export class EventLabelAssignmentsController
{
    constructor()
    {
        /** @type {EventLabelAssignmentsElements} */
        this.elements = new EventLabelAssignmentsElements();
        this.api = new ApiEventLabels();
        this.spinnerButton = new SpinnerButton(this.elements.eSubmitBtn);

        this.boardController = new RecurrencesBoardActionsController();
    }

    addListeners = () =>
    {
        this._listenForModalClose();
        this._listenForCheckboxCheckedChange();
        this._listenForFormSubmission();
    }


    //#region Reset modal

    _listenForModalClose = () =>
    {
        $(this.elements.eModal).on('hidden.bs.modal', (e) =>
        {
            this._resetModal();
        });
    }

    _resetModal = () =>
    {
        this._showLoading(); 
        this._disableSubmitButton();

        for (const checkbox of this.elements.eCheckboxes)
        {
            checkbox.checked = false;
        }
    }

    //#endregion

    //#region Listen for checkbox changes

    _listenForCheckboxCheckedChange = () =>
    {
        for(const checkbox of this.elements.eCheckboxes)
        {
            checkbox.addEventListener('change', (e) => 
            {
                this._enableSubmitButton();
            });
        }
    }

    //#endregion

    //#region Show event labels

    showEventLabels = async (eventId) =>
    {
        this.elements.setEventIdAttr(eventId);

        this._showLoading();
        this._showModal();

        const response = await this.api.get(eventId);

        const assignedLabels = await response.json();
        this._checkAssignedLabelCheckboxes(assignedLabels);

        this._hideLoading();
    }

    /**
     * Mark the checkboxes as 'checked' for all the checkboxes that are within the specified list of labels.
     * @param {Array<Label>} assignedLabels 
     */
    _checkAssignedLabelCheckboxes = (assignedLabels) =>
    {
        for (const label of assignedLabels) 
        {
            const checkbox = this._getCheckboxElementByValue(label.id);
            checkbox.checked = true;
        }
    }

    /**
     * Get the checkbox that has the label id value
     * @param {String} labelId the label id
     * @returns {HTMLInputElement}
     */
    _getCheckboxElementByValue = (labelId) =>
    {
        const checkbox = this.elements.eForm.querySelector(`input[name="${EventLabelAssignmentsElements.CHECKBOX_NAME}"][value="${labelId}"]`);
        return checkbox;
    }

    //#endregion

    //#region Update assigned labels

    /**
     * Listen for the form submission
     */
    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (e) =>
        {
            e.preventDefault();
            this._submitForm();
        });
    }

    /**
     * Submit the api request
     */
    _submitForm = async () =>
    {
        this.spinnerButton.showSpinner();

        const labels = this._getCheckedLabelIds();
        const eventId = this.elements.getEventIdAttr();
        
        const response = await this.api.putBatch(eventId, labels);

        this._handleBatchPutApiResponse(response);

        // refresh the board
        await this.boardController.getWeeklyRecurrences();

        this.spinnerButton.reset();
        this._hideModal();
    }


    /**
     * Get the ids of all the checked checkboxes in the form
     * @returns {Array<String>}
     */
    _getCheckedLabelIds = () =>
    {
        const labels = [];
        const checkedBoxes = this.elements.eForm.querySelectorAll(EventLabelAssignmentsElements.SELECTOR_CHECKED_CHECKBOXES);

        for (const checkbox of checkedBoxes)
        {
            const labelId = checkbox.value;
            labels.push(labelId);
        }

        return labels;
    }

    /**
     * Handle the api response to the batch put request for labels
     * @param {Promise<Response>} response the api response
     */
    _handleBatchPutApiResponse = async (response) =>
    {
        let alertTop = new AlertPageTopSuccess('Saved!');

        if (!response.ok)
        {
            const errorMessage = await response.text();
            console.error(errorMessage);

            alertTop = new AlertPageTopDanger('There was an error saving. Check console.');
        }
 
        alertTop.show();
    }

    //#endregion


    //#region Togglers

    _showModal = () => $(this.elements.eModal).modal('show');
    _hideModal = () => $(this.elements.eModal).modal('hide');

    _showLoading = () => this.elements.eModal.classList.add('loading')
    _hideLoading = () => this.elements.eModal.classList.remove('loading');

    _disableSubmitButton = () => this.elements.eSubmitBtn.disabled = true;
    _enableSubmitButton = () => this.elements.eSubmitBtn.disabled = false;

    //#endregion
}