import { Component, OnInit, ContentChildren, QueryList, AfterContentInit, ElementRef, TemplateRef } from '@angular/core';

import { PageSubheaderTitleDirective } from '../page-subheader/page-subheader-title.directive';
import { PageSubheaderSummaryDirective } from '../page-subheader/page-subheader-summary.directive';
import { ContextualTitleDirective } from '@shared/modules/ngx-contextual-help/contextual-title.directive';
import { ContextualContentDirective } from '@shared/modules/ngx-contextual-help/contextual-content.directive';

@Component({
  selector: 'app-page-subheader',
  templateUrl: './page-subheader.component.html',
  styleUrls: ['./page-subheader.component.scss']
})
export class PageSubheaderComponent implements OnInit, AfterContentInit {
  @ContentChildren(PageSubheaderTitleDirective, { descendants: true })
  public pageSubheaderTitleChildren: QueryList<PageSubheaderTitleDirective>;
  @ContentChildren(PageSubheaderSummaryDirective, { descendants: true })
  public pageSubheaderSummaryChildren: QueryList<PageSubheaderSummaryDirective>;
  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualContentChildren: QueryList<ContextualContentDirective>;

  constructor() { }

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

  public ngOnInit() { }

  public ngAfterContentInit() {
    // console.log(this.contextualTitleChildren.length);
    // console.log(this.contextualContentChildren.length);
    console.log(this.pageSubheaderSummaryChildren.length);
  }
}
