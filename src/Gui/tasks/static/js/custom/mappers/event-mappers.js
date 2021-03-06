
import { Event } from "../domain/models/event";
import { EventModalFormValues } from "../domain/forms/event-modal-form-values";
import { Utililties } from "../helpers/utilities";

export class EventMapper
{

    /**
     * Maps the given event modal form value object to an Event domain model
     * @param {EventModalFormValues} eventModalFormValues - form values
     * @returns {Event}
     */
    static ToModelFromFormValues(eventModalFormValues) {
        const model = new Event();

        model.name            = Utililties.isNullOrEmpty(eventModalFormValues.name) ? null : eventModalFormValues.name;
        model.phoneNumber     = Utililties.isNullOrEmpty(eventModalFormValues.phoneNumber) ? null : eventModalFormValues.phoneNumber;
        model.location        = Utililties.isNullOrEmpty(eventModalFormValues.location) ? null : eventModalFormValues.location;
        model.startsAt        = Utililties.isNullOrEmpty(eventModalFormValues.startsAt) ? null : eventModalFormValues.startsAt;
        model.endsAt          = Utililties.isNullOrEmpty(eventModalFormValues.endsAt) ? null : eventModalFormValues.endsAt;
        model.frequency       = Utililties.isNullOrEmpty(eventModalFormValues.frequency) ? null : eventModalFormValues.frequency;
        model.separation      = Utililties.isNullOrEmpty(eventModalFormValues.separation) ? null : eventModalFormValues.separation;
        model.recurrenceDay   = Utililties.isNullOrEmpty(eventModalFormValues.recurrenceDay) ? null : eventModalFormValues.recurrenceDay;
        model.recurrenceWeek  = Utililties.isNullOrEmpty(eventModalFormValues.recurrenceWeek) ? null : eventModalFormValues.recurrenceWeek;
        model.recurrenceMonth = Utililties.isNullOrEmpty(eventModalFormValues.recurrenceMonth) ? null : eventModalFormValues.recurrenceMonth;
        model.startsOn        = Utililties.isNullOrEmpty(eventModalFormValues.startsOn) ? null : eventModalFormValues.startsOn;
        model.endsOn          = Utililties.isNullOrEmpty(eventModalFormValues.endsOn) ? null : eventModalFormValues.endsOn;

        return model;
    }

    /**
     * Map the given api response data to an Event domain model
     * @param {object} apiResponseData the response data from the api
     * @returns {Event}
     */
    static ToModelFromApiGetRequest(apiResponseData) {
        const model = new Event();
        const modelKeys = Object.keys(model);

        for (const apiKey in apiResponseData) {
            if (!modelKeys.includes(apiKey)) {
                continue;
            }

            model[apiKey] = apiResponseData[apiKey];
        }

        return model;
    }

}