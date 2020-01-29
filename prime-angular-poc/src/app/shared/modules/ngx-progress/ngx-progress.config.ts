import { NgProgressConfig } from '@ngx-progressbar/core';

// For more configuration settings:
// @see https://github.com/MurhafSousli/ngx-progressbar
// Bar styles are handled by:
// @see /scss/vendors/_ngx-progress-bar.scss
export const ProgressConfig = {
  spinner: true,
  spinnerPosition: 'right',
  color: '#fcba19',
  thick: false,
  meteor: false,
  fixed: false
} as NgProgressConfig;
