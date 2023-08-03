import { BaseEventDetail } from "./detail";


export class BaseEvent
{
    /**
     * Event name (nameof)
     */
    static get EventName() 
    {
        return this.prototype.constructor.name;
    }

    /**
     * Invoke this event
     * @param {any} caller the calling object
     * @param {any} data the data to pass to the subscriber
     */
    // static invoke(caller, data=null)
    static invoke(caller=null, data=null)
    {
        const eventName = this.prototype.constructor.name;
        const eventDetail = new BaseEventDetail(caller, data);

        const customEvent = new CustomEvent(eventName, {
            detail: eventDetail,
            bubbles: true,
            cancelable: true,
        });

        const body = document.querySelector('body');
        body.dispatchEvent(customEvent);
    }

    /**
     * Add an event listener to the event
     * @param {function(BaseEventDetail)} callback 
     */
    static addListener(callback)
    {
        const body = document.querySelector('body');
        const eventName = this.prototype.constructor.name;

        body.addEventListener(eventName, (e) =>
        {
            callback(e.detail);
        });
    }
}
