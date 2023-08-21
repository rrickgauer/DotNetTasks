import { NativeEvents } from "../../../domain/constants/native-events";
import { ChecklistLabelServices } from "../../../services/checklist-label-services";




export class ChecklistLabelElements
{
    static ContainerClass = 'form-check-checklist-label';

    constructor(container)
    {
        /** @type {HTMLDivElement} */
        this.container = container;

        /** @type {HTMLInputElement} */
        this.checkbox = this.container.querySelector('input[type="checkbox"]');
    }
}



export class ChecklistLabel
{
    /** @type {string} */
    checklistId;

    /** @type {ChecklistLabelElements} */
    elements;

    /** @type {ChecklistLabelServices} */
    checklistLabelService;

    
    get labelId()
    {
        return this.elements.checkbox.value;
    }

    get isAssigned()
    {
        return this.elements.checkbox.checked;
    }

    set isAssigned(value)
    {
        this.elements.checkbox.checked = value;
    }

    constructor(checklistId, containerElement)
    {
        this.checklistId = checklistId;
        this.elements = new ChecklistLabelElements(containerElement);
        this.checklistLabelService = new ChecklistLabelServices(this.checklistId);

        this.#addListeners();
    }




    #addListeners = () =>
    {
        this.elements.checkbox.addEventListener(NativeEvents.Change, (e) => {
            this.#saveAssignmentChange();
        });
    }


    #saveAssignmentChange = async () =>
    {
        if (this.isAssigned)
        {
            await this.checklistLabelService.assignLabel(this.labelId);
        }
        else
        {
            await this.checklistLabelService.deleteAssignment(this.labelId);
        }
    }

}