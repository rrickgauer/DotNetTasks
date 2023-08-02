"use strict";

import { Utililties } from "../../helpers/utilities";


export class ChecklistsPageUrlWrapper
{
    //#region Static Stuff

    static SearchParmsKey = 'openChecklists';

    /**
     * Create a new instance using the current window's url
     */
    static fromCurrentUrl = () =>
    {
        const url = new URL(window.location.href);
        return new ChecklistsPageUrlWrapper(url);
    }

    //#endregion

    /**
     * Constructor
     * @param {URL} url 
     */
    constructor(url)
    {
        /** @type {URL} */
        this.url = url;
    }

    //#region Public Methods 

    /**
     * Add the checklist id to the url
     * @param {string} checklistId the checklist id to add
     */
    add = (checklistId) =>
    {
        const upddatedChecklistIds = this.#addChecklistIdToList(checklistId);
        this.#updateOpenChecklistIds(upddatedChecklistIds);
    }


    /**
     * Remove the checklist from the url
     * @param {string} checklistId the checklist to remove
     */
    remove = (checklistId) =>
    {
        const filteredChecklistIds = this.#removeChecklistId(checklistId);
        this.#updateOpenChecklistIds(filteredChecklistIds);
    }

    
    /**
     * Get a list of all the open checklist ids from the url
     * @returns the list of open checklist ids
     */
    getOpenChecklistIds = () =>
    {
        const rawText = this.url.searchParams.get(ChecklistsPageUrlWrapper.SearchParmsKey);

        if (Utililties.isNullOrEmpty(rawText))
        {
            return [];
        }

        const ids = rawText.split(',');

        return ids;
    }

    //#endregion

    //#region Private Methods

    /**
     * Add the specified checklist id to the url
     * @param {string} newChecklistId the checklistId to add
     * @returns the new list of ids
     */
    #addChecklistIdToList = (newChecklistId) =>
    {
        const openChecklists = this.getOpenChecklistIds();

        if (!openChecklists.includes(newChecklistId))
        {
            openChecklists.push(newChecklistId);
        }

        return openChecklists;
    }


    /**
     * Remove the specified checklist id
     * @param {string} checklistId the checklist id to remove
     * @returns the filtered list of open checklists
     */
    #removeChecklistId = (checklistId) =>
    {
        const checklistIds = this.getOpenChecklistIds();
        const filteredChecklistIds = checklistIds.filter(id => id !== checklistId);

        return filteredChecklistIds;
    }

    
    /**
     * Update the url query parm value to the list of specified checklist ids
     * @param {string[]} checklistIds list of checklist ids to set in the url
     */
    #updateOpenChecklistIds = (checklistIds) =>
    {
        if (checklistIds.length == 0)
        {
            this.#clearOutSearchParm();  // clear out parm completely
        }
        else
        {
            const newParmValues = Utililties.listToString(checklistIds, ',');
            this.#setOpenChecklistsUrlParm(newParmValues);              // set the parm value
        }
        
        // refresh the url without refreshing page
        this.#refreshUrl();
    }


    /**
     * Remove the url ?openChecklists search parm from the url
     */
    #clearOutSearchParm = () =>
    {
        this.url.searchParams.delete(ChecklistsPageUrlWrapper.SearchParmsKey);
    }


    /**
     * Set the new parm values
     * @param {string} parmValue 
     */
    #setOpenChecklistsUrlParm = (parmValue) =>
    {
        this.url.searchParams.set(ChecklistsPageUrlWrapper.SearchParmsKey, parmValue);
    } 


    /**
     * Refresh the window's url without refreshing the page
     */
    #refreshUrl = () =>
    {
        // Create a new URL with the updated search parameters
        this.url = new URL(`${this.url.origin}${this.url.pathname}?${this.url.searchParams.toString()}`);

        // update it without refreshing the page
        history.pushState(null, '', this.url);
    }


    //#endregion
    
}