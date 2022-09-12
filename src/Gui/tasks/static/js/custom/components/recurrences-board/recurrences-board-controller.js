import { RecurrencesBoardActionButtons } from "./recurrences-board-action-buttons";
import { DateTimeUtil } from "../../helpers/datetime";
import { UrlMethods } from "../../helpers/url-methods";
import { DateTime } from "../../../lib/luxon";
import { ApiRecurrences } from "../../api/api-recurrences";

/**
 * This class handles all the recurrences board action buttons and what happens when you click them.
 */
export class RecurrencesBoardActionsController 
{
    /**
     * Constructor
     */
    constructor() 
    {
        this.actionButtons = new RecurrencesBoardActionButtons();
    }

    /**
     * Add the event listeners to the page
     */
    addListeners = () => 
    {
        // set the current recurrences date to today's value
        this.actionButtons.todayButton.addEventListener('click', (e) => 
        {
            this._setDateValueToday();
            this._setUrlDateValueToValue();
        });

        // listen for recurrences date input value changes
        this.actionButtons.dateInput.addEventListener('change', (e) => 
        {
            this._setUrlDateValueToValue();
        });
    }

    //#region Set the previous and next week button href values

    /**
     * Set the previous and next week anchor button href values
     */
    setJumpWeekLinkValues = () =>
    {
        const labelsUrlParm = new URL(window.location.href).searchParams.get('labels');

        if (labelsUrlParm === null)
        {
            return;
        }

        this._setJumpButtonLinkHref(this.actionButtons.previousButton, labelsUrlParm);
        this._setJumpButtonLinkHref(this.actionButtons.nextButton, labelsUrlParm);
    }

    /**
     * 
     * @param {HTMLAnchorElement} eButtonAnchor 
     * @param {String} labelsUrlParm 
     */
    _setJumpButtonLinkHref = (eButtonAnchor, labelsUrlParm) =>
    {
        const prevButtonUrl = new URL(eButtonAnchor.href);
        prevButtonUrl.searchParams.set('labels', labelsUrlParm);
        eButtonAnchor.href = prevButtonUrl.toString();
    }

    //#endregion

    /**
     * Set the weekly recurrences board html
     */
    getWeeklyRecurrences = async () => 
    {
        const dateVal = this.getDateValue();
        const urlLabelsParm = new URL(window.location.href).searchParams.get('labels');

        const api = new ApiRecurrences();
        const response = await api.get(dateVal, urlLabelsParm);
        const recurrencesHtml = await response.text();

        this.hideSpinner();
        this.setBoardHtml(recurrencesHtml);
    }

    /**
     * Set the curernt value of the date input to today's date
     */
    _setDateValueToday = () => 
    {
        const currentDate = DateTimeUtil.getCurrentDateIso();
        this.setDateValue(currentDate);
    }

    /**
     * Set the URL's query parm to the value of the date input
     */
    _setUrlDateValueToValue = () => 
    {
        const newDate = this.actionButtons.dateInput.value;
        const newUrl = UrlMethods.setQueryParmAndRefresh('d', newDate);
        window.location.href = newUrl;
    }

    /**
     * Hide the recurrences board spinner section
     */
    hideSpinner = () => this.actionButtons.spinner.classList.add('d-none');


    /**
     * Render the given recurrences board html to the page.
     * @param {string} dailyRecurrencesHtml the html to render to the page
     */
    setBoardHtml = (dailyRecurrencesHtml) => this.actionButtons.container.innerHTML = dailyRecurrencesHtml;


    jumpToNextWeek = () => this.actionButtons.nextButton.click();
    jumpToPreviousWeek = () => this.actionButtons.previousButton.click();
    jumpToCurrentDate = () => this.actionButtons.todayButton.click();


    /**
     * Get the current date input value as a Luxon DateTime
     * @returns {DateTime}
     */
    getDateValue = () => 
    {
        const currentValue = this.actionButtons.dateInput.value;
        return DateTimeUtil.toDateTime(currentValue);
    }

    /**
     * Set the current value of the date input
     * @param {string} newDateValue - the new date value
     */
    setDateValue = (newDateValue) => this.actionButtons.dateInput.value = newDateValue;

    /**
     * Checks if the collapse menu toggle button is visisble
     * @returns {bool}
     */
    isCollpaseButtonVisibile = () => this.actionButtons.collapseButton.offsetParent == null;

    /** Show the collapse menu */
    showCollapseMenu = () => this.actionButtons.collapseMenu.classList.add('show');

    /** Hide the collapse menu */
    hideCollapseMenu = () => this.actionButtons.collapseMenu.classList.remove('show');
}