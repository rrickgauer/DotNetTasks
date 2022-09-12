import { ApiLabels } from "../../../api/api-labels";
import { Label } from "../../../domain/models/label";
import { EditLabelPageFormController } from "../label-form/edit-label-form-controller";
import { LabelsListItemElements } from "./labels-list-item-elements";


export class LabelsListItemController
{
    constructor()
    {
        this.editLabelFormController = new EditLabelPageFormController();
        this.api = new ApiLabels();
    }


    addListeners = () =>
    {
        this._listenForDropdownMenuItemClick();
    }

    _listenForDropdownMenuItemClick = () =>
    {

        document.addEventListener('click', (event) =>
        {
            if (!event.target.classList.contains(LabelsListItemElements.DROPDOWN_BTN)) 
            {
                return;
            }

            this._handleDropdownMenuItemClick(event.target);
        });

    }

    /**
     * 
     * @param {HTMLButtonElement} eClickedDropdownBtn the clicked dropdown button
     */
    _handleDropdownMenuItemClick = async (eClickedDropdownBtn) =>
    {
        const dropdownActionValue = eClickedDropdownBtn.getAttribute(LabelsListItemElements.DROPDOWN_BTN_ATTR);

        const eContainer = eClickedDropdownBtn.closest(`.${LabelsListItemElements.CONTAINER}`);
        const labelsListItem = new LabelsListItemElements(eContainer);

        switch(dropdownActionValue)
        {
            case LabelsListItemElements.DropdownBtnAttrs.EDIT:
                await this._editLabel(labelsListItem);
                break;
            
            case LabelsListItemElements.DropdownBtnAttrs.DELETE:
                await this._deleteLabel(labelsListItem);
                break;
        }
    }

    //#region Edit label

    /**
     * Edit the given LabelsListItemElements object
     * Open up the edit label form with the appropriate info in in.
     * @param {LabelsListItemElements} labelsListItem the label to edit
     */
    _editLabel = async (labelsListItem) =>
    {
        // show the form in loading mode
        this.editLabelFormController.elements.showLoading();
        this.editLabelFormController.showModal();

        // fetch the data from the api
        const labelData = await this._getLabelFromApi(labelsListItem.id);
        
        // show the data in the form inputs
        this.editLabelFormController.setFormValues(labelData);
        this.editLabelFormController.elements.hideLoading();
    }


    /**
     * Get the specified label from the api
     * @param {String} labelId the label id
     * @returns {Promise<Label>}
     */
    _getLabelFromApi = async (labelId) =>
    {
        const response = await this.api.get(labelId);
        const data = await response.json();

        const label = new Label();

        label.color     = data.data.color;
        label.createdOn = data.data.createdOn;
        label.id        = data.data.id;
        label.name      = data.data.name;
        label.userId    = data.data.userId;

        return label;
    }

    //#endregion

    //#region Delete label

    /**
     * Delete the label
     * @param {LabelsListItemElements} labelsListItem the list item to delete
     */
    _deleteLabel = async (labelsListItem) =>
    {
        if (!confirm('Are you sure? This cannot be undone.'))
        {
            return;
        }

        const labelId = labelsListItem.id;
        
        this.api.delete(labelId);

        labelsListItem.eContainer.remove();
    }

    //#endregion

}