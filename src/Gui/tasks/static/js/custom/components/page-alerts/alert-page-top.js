import { AlertTypes } from "./alert-types";



//#region Base class

export class AlertPageTopBase
{
    /**
     * Constructor
     * @param {string} message the message to display
     */
    constructor(message, alertType)
    {
        /** @type {string} */
        this.message = message;

        /** @type {string} */
        this.alertType = alertType;

        /** @type {HTMLDivElement} */
        this._eContainer = document.getElementById(AlertPageTopBase.Html.CONTAINER);
    }

    /**
     * Show the alert
     */
    show = () =>
    {
        this._eContainer.innerHTML = this._getHtml();
    }

    /**
     * Get the html string to display on the page.
     * @returns {string}
     */
    _getHtml = () =>
    {
        let html = `
            <div class="alert ${this.alertType} alert-dismissible show" role="alert">
                <div id="alert-page-top-text">${this.message}</div>
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
        `;

        return html;
    }
}

AlertPageTopBase.Html = 
{
    CONTAINER: 'alerts-page-top',
}

//#endregion


export class AlertPageTopSuccess extends AlertPageTopBase
{
    /**
     * Display a successful alert.
     * @param {string} message the message to display
     */
    constructor(message)
    {
        super(message, AlertTypes.SUCCESS);
    }
}


export class AlertPageTopDanger extends AlertPageTopBase
{
    /**
     * Display a danger (red) alert.
     * @param {string} message the message to display
     */
    constructor(message)
    {
        super(message, AlertTypes.DANGER);
    }
}



