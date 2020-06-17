import { Component, OnInit, ContentChildren, QueryList } from '@angular/core';

import { PageSubheader2SummaryDirective } from './page-subheader2-summary.directive';
import { PageSubheader2MoreInfoDirective } from './page-subheader2-more-info.directive';

@Component({
  selector: 'app-page-subheader2',
  templateUrl: './page-subheader2.component.html',
  styleUrls: ['./page-subheader2.component.scss']
})
export class PageSubheader2Component implements OnInit {
  @ContentChildren(PageSubheader2SummaryDirective, { descendants: true })
  public pageSubheaderSummaryChildren: QueryList<PageSubheader2SummaryDirective>;
  @ContentChildren(PageSubheader2MoreInfoDirective, { descendants: true })
  public pageSubheaderMoreInfoChildren: QueryList<PageSubheader2MoreInfoDirective>;

  public showMoreInfo: boolean;

  constructor() { }

  public get hasPageSubheaderSummary(): boolean {
    return !!this.pageSubheaderSummaryChildren.length;
  }

  public get hasPageSubheaderMoreInfo(): boolean {
    return !!this.pageSubheaderMoreInfoChildren.length;
  }

  public toggleMoreInfo(): boolean {
    return this.showMoreInfo = !this.showMoreInfo;
  }

  public ngOnInit(): void { }
}
