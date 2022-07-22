
import { EventModalForm } from "../../components/event-modal/form";


$(document).ready(function() {
    addListeners();
});


function addListeners() {
    $('#test-btn').on('click', printValues);
}

function printValues() {
    const form = new EventModalForm();
    const formValues = form.getValues();
    console.log(formValues);
}

