import { LoadingContainer } from "../../../helpers/loading-container";
import { UrlMethods } from "../../../helpers/url-methods";
import { ChecklistLabel, ChecklistLabelElements } from "./checklist-label";






export class LabelsPageElements
{

    static ClassContainer = 'checklist-labels-container';
    static IdForm = 'checklist-labels-form';

    constructor()
    {
        /** @type {HTMLDivElement} */
        this.container = document.querySelector(`.${LabelsPageElements.ClassContainer}`);
        
        /** @type {HTMLFormElement} */
        this.form = this.container.querySelector(`#${LabelsPageElements.IdForm}`);
    }
}




export class LabelsPageController
{

    get checklistId()
    {
        return UrlMethods.getPathValue(1);
    }

    constructor()
    {
        this.elements = new LabelsPageElements();

        /** @type {ChecklistLabel[]} */
        this.labels = this.#getLabels();


    }


    init = () =>
    {
        
    }

    #getLabels = () =>
    {
        const labelElements = this.elements.form.querySelectorAll(`.${ChecklistLabelElements.ContainerClass}`);

        const result = [];

        for (const element of labelElements)
        {
            const checklistLabel = new ChecklistLabel(this.checklistId, element);
            checklistLabel.init();

            result.push(checklistLabel);
        }

        return result;
    }
}