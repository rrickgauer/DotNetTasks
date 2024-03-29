import { RecurrencesBoardFilterModalElements } from "./recurrences-board-filter-modal-elements";

export class RecurrencesBoardFilterModalController
{
    constructor()
    {
        this.elements = new RecurrencesBoardFilterModalElements();
    }


    addListeners = () =>
    {
        this._listenForFormSubmission();
    }

    //#region Form submission

    /**
     * Event listener for submitting the form
     */
    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (e) =>
        {
            e.preventDefault();
            this._submitForm();
        });
    }

    /**
     * Submit the form
     */
    _submitForm = () =>
    {
        const url = new URL(window.location.href);
        url.searchParams.delete('labels');

        const checkedLabelIds = this._getCheckedLabelIds();
        
        if (checkedLabelIds.length == 0)
        {    
            window.location.href = url;
            return;
        }

        // create the new url labels search parm value
        let searchParmValue = this._generateLabelsUrlSeachParmsValue(checkedLabelIds);
        url.searchParams.set('labels', searchParmValue);

        // refresh the page
        window.location.href = url;
    }

    /**
     * Get the ids of the currently checked label inputs
     * @returns {Array}
     */
    _getCheckedLabelIds = () =>
    {
        const checkedLabelElements = this.elements.eForm.querySelectorAll(`input[name='labels']:checked`);

        const labelIds = [];

        for(const e of checkedLabelElements)
        {
            labelIds.push(e.value);
        }

        return labelIds;
    }

    /**
     * Combine all the label ids into a string seperated by a comma
     * @param {Array} labelIds list of label ids to
     * @returns {String}
     */
    _generateLabelsUrlSeachParmsValue = (labelIds) =>
    {
        let searchParmValue = '';
        let isFirst = true;

        for (const labelId of labelIds) {
            
            if (isFirst)
            {
                isFirst = false;
                searchParmValue = labelId;
                continue;
            }
            
            searchParmValue += `,${labelId}`;
        }

        return searchParmValue;
    }

    //#endregion

    
    /**
     * Mark the specified label filter checkboxes as checked if they are present in the current URL search parms.
     */
    setCheckedLabelsFromUrlParm = () =>
    {
        const url = new URL(window.location.href);
        
        if (!url.searchParams.has('labels'))
        {
            return;
        }

        const urlParmValues = url.searchParams.get('labels').split(',');

        for(const labelId of urlParmValues)
        {
            let checkbox = this.elements.eForm.querySelector(`input[name="labels"][value="${labelId}"]`);
            checkbox.checked = true;
        }        
    }
}