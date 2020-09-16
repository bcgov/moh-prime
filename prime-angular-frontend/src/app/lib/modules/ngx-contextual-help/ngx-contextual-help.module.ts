import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

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
