import { Component, OnInit } from '@angular/core';

import { AbstractCollectionNoticeAlert } from '@shared/components/collection-notice-container/collection-notice-container.component';

@Component({
  selector: 'app-enrolment-collection-notice',
  template: '',
  styleUrls: []
})
export class EnrolmentCollectionNoticeComponent extends AbstractCollectionNoticeAlert implements OnInit {
  constructor() {
    super();
  }

  public onAccept() {

  }

  public ngOnInit(): void {
    // Display of the Collection Notice has moved to PrimeEnrolmentAccessComponent
    // so we jump through this component but leave it in place for other routing logic
    this.accepted.emit();
  }
}
