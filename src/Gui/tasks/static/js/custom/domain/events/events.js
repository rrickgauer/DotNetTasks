import { BaseEvent } from "./base-event";


export class NewChecklistFormSubmittedEvent extends BaseEvent {}
export class NewChecklistFormToggleEvent extends BaseEvent {}


export class ChecklistsSidebarOpenedEvent extends BaseEvent {}
export class ChecklistsSidebarClosedEvent extends BaseEvent {}

export class ChecklistsOverlayClickedEvent extends BaseEvent {}

export class ChecklistsSidebarItemOpenedEvent extends BaseEvent {}
export class ChecklistsSidebarItemClosedEvent extends BaseEvent {}


export class OpenChecklistCloseButtonClickedEvent extends BaseEvent {}

export class DeleteChecklistEvent extends BaseEvent {}


// checklist settings page
export class ChecklistSettingsGeneralFormSubmittedEvent extends BaseEvent {}
export class ChecklistSettingsChecklistDeletedEvent extends BaseEvent {}
export class ChecklistSettingsChecklistClonedEvent extends BaseEvent {}


// checklist items
export class ChecklistItemDeleteButtonClickedEvent extends BaseEvent {}
export class ChecklistItemMoveItemUpButtonClickedEvent extends BaseEvent {}
export class ChecklistItemMoveItemDownButtonClickedEvent extends BaseEvent {}