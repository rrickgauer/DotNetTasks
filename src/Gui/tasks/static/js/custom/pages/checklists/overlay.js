import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistsOverlayClickedEvent } from "../../domain/events/events";


export class ChecklistsOverlay
{
    static OverlayClass = 'drawer-overlay';
    static eOverlay = `<div style="z-index: 109;" class="${ChecklistsOverlay.OverlayClass}"></div>`;

    constructor()
    {
        this.#addListeners();
    }

    #addListeners = () =>
    {
        this.#listenForOverlayClick();
    }

    #listenForOverlayClick = () =>
    {
        document.querySelector('body').addEventListener(NativeEvents.CLICK, (e) => 
        {
            if (e.target.classList.contains(ChecklistsOverlay.OverlayClass))
            {
                ChecklistsOverlayClickedEvent.invoke(this, null);
            }
        });
    }


    show = () =>
    {
        $('body').append(ChecklistsOverlay.eOverlay);
    }

    remove = () =>
    {
        document.querySelector(`.${ChecklistsOverlay.OverlayClass}`).remove();
    }

}