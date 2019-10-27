
import { InjectionToken } from '@angular/core';
import { DialogDefaultContent } from './dialog-default-content.model';

// TODO: turn this into a service instead
// Reuseable default dialog content
export const defaultDialogContent: DialogDefaultContent = {
  unsaved: () => ({
    title: 'Unsaved Changes Found',
    message: 'Changes are pending that have not been saved. Would you like to remain on this page until it has been completed and saved, or discard your changes?',
    actionText: 'Discard Changes',
    cancelText: 'Keep Changes and Continue'
  })
};

export const DIALOG_DEFAULT_OPTION = new InjectionToken('DIALOG_DEFAULT_OPTION', {
  providedIn: 'root',
  factory: () => defaultDialogContent
});
