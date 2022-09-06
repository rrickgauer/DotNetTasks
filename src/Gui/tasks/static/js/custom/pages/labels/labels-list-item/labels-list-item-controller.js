import { EditLabelPageFormController } from "../label-form/edit-label-form-controller";
import { LabelsListItemElements } from "./labels-list-item-elements";


export class LabelsListItemController
{
    constructor()
    {
        this.editLabelFormController = new EditLabelPageFormController();
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
    _handleDropdownMenuItemClick = (eClickedDropdownBtn) =>
    {
        const dropdownActionValue = eClickedDropdownBtn.getAttribute(LabelsListItemElements.DROPDOWN_BTN_ATTR);

        const eContainer = eClickedDropdownBtn.closest(`.${LabelsListItemElements.CONTAINER}`);
        const labelsListItem = new LabelsListItemElements(eContainer);

        switch(dropdownActionValue)
        {
            case LabelsListItemElements.DropdownBtnAttrs.EDIT:
                this._editLabel(labelsListItem);
                break;
            
            case LabelsListItemElements.DropdownBtnAttrs.DELETE:
                // delete
                break;
        }
    }


    /**
     * Edit the given LabelsListItemElements object
     * Open up the edit label form with the appropriate info in in.
     * @param {LabelsListItemElements} labelsListItem the label to edit
     */
    _editLabel = async (labelsListItem) =>
    {
        console.log(labelsListItem);

        this.editLabelFormController.showModal();
    }


    



}