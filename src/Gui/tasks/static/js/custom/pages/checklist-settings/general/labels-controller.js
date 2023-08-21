import { UrlMethods } from "../../../helpers/url-methods";
import { ChecklistLabel, ChecklistLabelElements } from "./checklist-label";


export class LabelsControllerElements
{
    //#region Static Fields
    static ClassContainer = 'checklist-labels-container';
    static IdForm = 'checklist-labels-form';
    //#endregion

    //#region Fields

    /** @type {HTMLDivElement} */
    container;
    
    /** @type {HTMLFormElement} */
    form;

    //#endregion 


    constructor()
    {
        this.container = document.querySelector(`.${LabelsControllerElements.ClassContainer}`);
        this.form = this.container.querySelector(`#${LabelsControllerElements.IdForm}`);
    }
}


export class LabelsController
{
    //#region Fields

    /** @type {LabelsControllerElements} */
    #elements;

    /** @type {ChecklistLabel[]} */
    #labels;

    //#endregion

    //#region Properties

    get checklistId()
    {
        return UrlMethods.getPathValue(1);
    }


    //#endregion

    /**
     * Constructor
     */
    constructor()
    {
        this.#elements = new LabelsControllerElements();
        this.#labels = this.#getLabels();
    }

    /**
     * Get all instances of the labels in the sidebar
     */
    #getLabels = () =>
    {
        const labelElements = this.#elements.form.querySelectorAll(`.${ChecklistLabelElements.ContainerClass}`);

        const result = [];

        for (const element of labelElements)
        {
            const checklistLabel = new ChecklistLabel(this.checklistId, element);
            result.push(checklistLabel);
        }

        return result;
    }
}