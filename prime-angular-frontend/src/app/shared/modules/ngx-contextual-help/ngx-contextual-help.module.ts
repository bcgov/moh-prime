import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatMenuModule, MatIconModule, MatButtonModule } from '@angular/material';

import { ContextualHelpComponent } from './contextual-help/contextual-help.component';

@NgModule({
  declarations: [
    ContextualHelpComponent
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule
  ],
  exports: [
    ContextualHelpComponent
  ]
})
export class NgxContextualHelpModule { }
