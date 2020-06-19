import { InjectionToken } from '@angular/core';

import { StringUtils } from '@lib/utils/string-utils.class';

import { DialogDefaultOptions } from './dialog-default-options.model';

// Reuseable default dialog content
export const defaultDialogOptions: DialogDefaultOptions = {
  unsaved: () => ({
    title: 'Unsaved Changes Found',
    message: `
    Changes are pending that have not been saved. Would you like to
    remain on this page until it has been completed and saved, or
    discard your changes?
    `,
    actionType: 'warn',
    actionText: 'Discard Changes',
    cancelText: 'Keep Changes and Continue'
  }),
  delete: (modelName: string) => {
    const capitalized = StringUtils.capitalize(modelName);
    return {
      title: `Delete ${capitalized}`,
      message: `Are you sure you want to delete this ${modelName.toLowerCase()}?`,
      actionType: 'warn',
      actionText: `Delete ${capitalized}`
    };
  }
};

export const DIALOG_DEFAULT_OPTION = new InjectionToken('DIALOG_DEFAULT_OPTION', {
  providedIn: 'root',
  factory: () => defaultDialogOptions
});
