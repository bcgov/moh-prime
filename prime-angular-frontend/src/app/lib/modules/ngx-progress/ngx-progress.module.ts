import { NgModule } from '@angular/core';

import { NgProgressModule } from 'ngx-progressbar';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { ProgressConfig } from './ngx-progress.config';

@NgModule({
  imports: [
    NgProgressModule.withConfig(ProgressConfig),
    NgProgressHttpModule
  ],
  exports: [
    NgProgressModule
  ]
})
export class NgxProgressModule { }
