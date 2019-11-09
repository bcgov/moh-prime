import { Component, OnInit, ContentChildren, QueryList, AfterContentInit, ElementRef } from '@angular/core';

import { ContextualTitleDirective } from '@shared/modules/ngx-contextual-help/contextual-title.directive';
import { ContextualContentDirective } from '@shared/modules/ngx-contextual-help/contextual-content.directive';

@Component({
  selector: 'app-page-sub-header',
  templateUrl: './page-sub-header.component.html',
  styleUrls: ['./page-sub-header.component.scss']
})
export class PageSubHeaderComponent implements OnInit, AfterContentInit {
  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualContentChildren: QueryList<ContextualContentDirective>;
  @ContentChildren('summary', { descendants: true })
  public subHeaderSummaryChildren: QueryList<ElementRef>;

  constructor() { }

  public get hasContextualTitle(): boolean {
    return !!this.contextualTitleChildren.length;
  }

  public get hasContextualContent(): boolean {
    return !!this.contextualContentChildren.length;
  }

  public get hasSubheaderSummary(): boolean {
    return !!this.contextualTitleChildren.length;
  }

  public ngOnInit() { }

  public ngAfterContentInit() { }
}
