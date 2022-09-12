


export class LabelsPageElements
{
    constructor()
    {
        /** @type {HTMLDivElement} */
        this.eSpinner = document.getElementById(LabelsPageElements.SPINNER);

        /** @type {HTMLDivElement} */
        this.eContent = document.getElementById(LabelsPageElements.CONTENT);
    }
}


LabelsPageElements.SPINNER = 'labels-wrapper-spinner';
LabelsPageElements.CONTENT = 'labels-content';