import { BootstrapModalMethods } from "../domain/constants/bootstrap-classes"



export class BootstrapUtilities {
    
    /**
     * Show the given modal
     * @param {HTMLElement} modal the modal element
     */
    static modalShow = (modal) => {
        $(modal).modal(BootstrapModalMethods.SHOW);
    }

    /**
     * Hide the given modal
     * @param {HTMLElement} modal the modal element
     */
    static modalHide = (modal) => {
        $(modal).modal(BootstrapModalMethods.HIDE);
    }

    /**
    * Toggle the given modal
    * @param {HTMLElement} modal the modal element
    */
    static modalToggle = (modal) => {
        $(modal).modal(BootstrapModalMethods.TOGGLE);
    }

}