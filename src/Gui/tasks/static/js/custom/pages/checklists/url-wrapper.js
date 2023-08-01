import { Utililties } from "../../helpers/utilities";



export class ChecklistsPageUrlWrapper
{

    static SearchParmsKey = 'openChecklists';

    static fromCurrentUrl = () =>
    {
        const url = new URL(window.location.href);
        return new ChecklistsPageUrlWrapper(url);
    }

    /**
     * Constructor
     * @param {URL} url 
     */
    constructor(url)
    {
        /** @type {URL} */
        this.url = url;
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



    addChecklistId = (newChecklistId) =>
    {
        /** @type {string[]} */
        const openChecklists = this.getOpenChecklistIds();

        // if (openChecklists.includes())

    }







    
}