import { Component, OnInit } from '@angular/core';

import { ICollectionNoticeAlert } from '@shared/components/collection-notice-alert/collection-notice-alert.component';

@Component({
  selector: 'app-site-collection-notice',
  templateUrl: './site-collection-notice.component.html',
  styleUrls: ['./site-collection-notice.component.scss']
})
export class SiteCollectionNoticeComponent extends ICollectionNoticeAlert implements OnInit {
  constructor() {
    super();
  }

  public onAccept() {
    this.accepted.emit();
  }

  public ngOnInit(): void { }
}
