"use strict"

import { AlertPageTopBase, AlertPageTopDanger, AlertPageTopSuccess } from "../../../components/page-alerts/alert-page-top";
import { ChecklistSettingsChecklistClonedEvent, ChecklistSettingsChecklistDeletedEvent, ChecklistSettingsGeneralFormSubmittedEvent } from "../../../domain/events/events";
import { ChecklistModel } from "../../../domain/models/checklist";
import { UrlMethods } from "../../../helpers/url-methods";
import { CloneChecklistController } from "./clone-checklist-controller";
import { DeleteChecklistController } from "./delete-checklist-controller";
import { ExportChecklistItemsController } from "./export-items-controller";
import { GeneralSettingsFormController } from "./general-settings-form-controller";
import { LabelsController } from "./labels-controller";


export class ChecklistSettingsPageController
{
    //#region - Fields -

    /** @type {string} */
    #checklistId;

    /** @type {GeneralSettingsFormController} */
    #generalSettingsForm;
    
    /** @type {DeleteChecklistController} */
    #deleteChecklistController;
    
    /** @type {CloneChecklistController} */
    #cloneChecklistController;
    
    /** @type {ExportChecklistItemsController} */
    #exportItemsController;
    
    /** @type {LabelsController} */
    #labelsController;

    //#endregion



    constructor()
    {
        this.#checklistId               = UrlMethods.getPathValue(1);
        this.#generalSettingsForm       = new GeneralSettingsFormController(this.#checklistId);
        this.#deleteChecklistController = new DeleteChecklistController(this.#checklistId);
        this.#cloneChecklistController  = new CloneChecklistController(this.#checklistId);
        this.#exportItemsController     = new ExportChecklistItemsController();
        this.#labelsController          = new LabelsController();
    }

    /**
     * Initialize all the fields
     */
    init = () =>
    {
        this.#generalSettingsForm.init();
        this.#deleteChecklistController.init();
        this.#cloneChecklistController.init();
        this.#exportItemsController.init();
        this.#addEventListeners();
    }


    /**
     * Register all the event listeners
     */
    #addEventListeners = () =>
    {
        ChecklistSettingsGeneralFormSubmittedEvent.addListener((e) => 
        {  
            this.#onChecklistSettingsGeneralFormSubmittedEvent(e.data);
        });

        ChecklistSettingsChecklistDeletedEvent.addListener((e) => 
        {
            window.location.href = '/checklists';
        });

        ChecklistSettingsChecklistClonedEvent.addListener((e) => 
        {
            this.#handleSuccessfulChecklistClone(e.data);
        });
        
    }

    /**
     * Handle the general checklist settings form event submission event
     * @param {boolean} successful 
     */
    #onChecklistSettingsGeneralFormSubmittedEvent = (successful) =>
    {
        /** @type {AlertPageTopBase} */
        let customAlert = new AlertPageTopSuccess('Changes saved successfully');

        if (!successful)
        {
            customAlert = new AlertPageTopDanger('There was an error saving the checklist. Please try again');
        }

        customAlert.show();
    }


    /**
     * Handle a successful clone
     * @param {ChecklistModel} newChecklist updated checklist
     */
    #handleSuccessfulChecklistClone = (newChecklist) =>
    {
        // build the url for the checklist
        const parms = new URLSearchParams();
        parms.set('openChecklists', newChecklist.id);

        const url = `/checklists?${parms.toString()}`;
        
        // wrap the link in an anchor tag to display in the alert
        const alertText = `Checklist was successfully cloned. <a href="${url}" class="alert-link">Click here to view it.</a>`;

        // show the alert with the link in it
        const successAlert = new AlertPageTopSuccess(alertText);
        successAlert.show();
    }

}