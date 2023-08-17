







export class LoadingContainer
{

    static ClassContainer = 'container-loading';
    static ClassSpinnerContainer = 'container-loading-spinner';
    static ClassContentContainer = 'container-loading-content';
    static ClassLoading = 'loading';



    /** @type {HTMLElement} */
    #container;

    /** @type {HTMLElement} */
    #spinnerContainer;

    /** @type {HTMLElement} */
    #contentContainer;

    constructor(container)
    {
        this.#container = container;
        this.#spinnerContainer = this.#container.querySelector(`.${LoadingContainer.ClassSpinnerContainer}`);
        this.#contentContainer = this.#container.querySelector(`.${LoadingContainer.ClassContentContainer}`);
    }

    

    get isLoading()
    {
        return this.#container.classList.contains(LoadingContainer.ClassLoading);
    }

    set isLoading(value)
    {
        if (value)
        {
            this.#container.classList.add(LoadingContainer.ClassLoading);
        }
        else
        {
            this.#container.classList.remove(LoadingContainer.ClassLoading);
        }
    }

}