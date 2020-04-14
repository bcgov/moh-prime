import { Component, OnInit } from '@angular/core';

import { ICollectionNoticeAlert } from '@shared/components/collection-notice-alert/collection-notice-alert.component';

@Component({
  selector: 'app-enrolment-collection-notice',
  templateUrl: './enrolment-collection-notice.component.html',
  styleUrls: ['./enrolment-collection-notice.component.scss']
})
export class EnrolmentCollectionNoticeComponent extends ICollectionNoticeAlert implements OnInit {
  constructor() {
    super();
  }

  public onAccept() {
    this.accepted.emit();
  }

  public ngOnInit(): void { }
}
