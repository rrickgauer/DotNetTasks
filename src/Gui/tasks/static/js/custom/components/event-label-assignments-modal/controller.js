import { ApiEventLabels } from "../../api/api-event-labels";
import { Label } from "../../domain/models/label";
import { EventLabelAssignmentsElements } from "./elements";



export class EventLabelAssignmentsController
{
    constructor()
    {
        /** @type {EventLabelAssignmentsElements} */
        this.elements = new EventLabelAssignmentsElements();
        this.api = new ApiEventLabels();
    }

    addListeners = () =>
    {
        this._listenForModalClose();
    }


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
        
        const checkboxes = this.elements.eForm.querySelectorAll(`input[type="checkbox"]`);

        for (const checkbox of checkboxes)
        {
            checkbox.checked = false;
        }
    }


    //#region Show event labels

    
    showEventLabels = async (eventId) =>
    {
        this._showLoading();
        this._showModal();

        const response = await this.api.get(eventId);

        const assignedLabels = await response.json();
        this._checkAssignedLabelCheckboxes(assignedLabels);

        this._hideLoading();
    }

    /**
     * 
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


    _showModal = () => $(this.elements.eModal).modal('show');
    _hideModal = () => $(this.elements.eModal).modal('hide');

    _showLoading = () => this.elements.eModal.classList.add('loading')
    _hideLoading = () => this.elements.eModal.classList.remove('loading');
}