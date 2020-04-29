import { Component, OnInit } from '@angular/core';

import { AbstractCollectionNoticeAlert } from '@shared/components/collection-notice-container/collection-notice-container.component';

@Component({
  selector: 'app-site-collection-notice',
  templateUrl: './site-collection-notice.component.html',
  styleUrls: ['./site-collection-notice.component.scss']
})
export class SiteCollectionNoticeComponent extends AbstractCollectionNoticeAlert implements OnInit {
  constructor() {
    super();
  }

  public onAccept() {
    this.accepted.emit();
  }

  public ngOnInit(): void { }
}
