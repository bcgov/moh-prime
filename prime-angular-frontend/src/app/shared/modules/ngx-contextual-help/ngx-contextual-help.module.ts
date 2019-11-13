import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatMenuModule, MatIconModule, MatButtonModule } from '@angular/material';

import { ContextualHelpComponent } from './contextual-help/contextual-help.component';
import { ContextualTitleDirective } from './contextual-title.directive';
import { ContextualContentDirective } from './contextual-content.directive';

@NgModule({
  declarations: [
    ContextualHelpComponent,
    ContextualTitleDirective,
    ContextualContentDirective
  ],
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule
  ],
  exports: [
    ContextualHelpComponent,
    ContextualTitleDirective,
    ContextualContentDirective
  ]
})
export class NgxContextualHelpModule { }
