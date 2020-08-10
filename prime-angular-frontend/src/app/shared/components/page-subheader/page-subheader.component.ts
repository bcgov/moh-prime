import { Component, OnInit, ContentChildren, QueryList, Input } from '@angular/core';

import { ContextualTitleDirective } from '@lib/modules/ngx-contextual-help/contextual-title.directive';
import { ContextualContentDirective } from '@lib/modules/ngx-contextual-help/contextual-content.directive';

import { PageSubheaderTitleDirective } from '../page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '../page-subheader/page-subheader-summary.directive';

@Component({
  selector: 'app-page-subheader',
  templateUrl: './page-subheader.component.html',
  styleUrls: ['./page-subheader.component.scss']
})
export class PageSubheaderComponent implements OnInit {
  @ContentChildren(PageSubheaderTitleDirective, { descendants: true })
  public pageSubheaderTitleChildren: QueryList<PageSubheaderTitleDirective>;
  @ContentChildren(PageSubheaderSummaryDirective, { descendants: true })
  public pageSubheaderSummaryChildren: QueryList<PageSubheaderSummaryDirective>;
  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualContentChildren: QueryList<ContextualContentDirective>;
  // Mode where verbose summaries that exceed the use of contextual tips can
  // be viewed using more information icon, but aren't initial displayed
  @Input() public summaryAsInfo: boolean;
  @Input() public divider: boolean;

  public showSummary: boolean;

  constructor() {
    this.summaryAsInfo = false;
    this.divider = true;
  }

  public get hasPageSubheaderTitle(): boolean {
    return !!this.pageSubheaderTitleChildren.length;
  }

  public get hasPageSubheaderSummary(): boolean {
    return !!this.pageSubheaderSummaryChildren.length;
  }

  public get hasContextualTitle(): boolean {
    return !!this.contextualTitleChildren.length;
  }

  public get hasContextualContent(): boolean {
    return !!this.contextualContentChildren.length;
  }

  public toggleSummary(): boolean {
    return this.showSummary = !this.showSummary;
  }

  public ngOnInit() {
    // Default always show the summary
    this.showSummary = !this.summaryAsInfo;
  }
}
