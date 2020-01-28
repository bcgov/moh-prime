import { NgModule } from '@angular/core';

import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';

import { ProgressConfig } from '@shared/modules/ngx-progress/ngx-progress.config';

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
