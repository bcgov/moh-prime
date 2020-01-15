import { Component, OnInit, ViewChild, AfterViewInit, Input } from '@angular/core';

import { Subscription } from 'rxjs';

import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit, AfterViewInit {
  @Input() public busy: Subscription;
  @ViewChild(PageHeaderComponent, { static: false }) public pageHeader: PageHeaderComponent;

  constructor() { }

  public ngOnInit() { }

  public ngAfterViewInit() {
    console.log('COUNT', this.pageHeader);
  }
}
