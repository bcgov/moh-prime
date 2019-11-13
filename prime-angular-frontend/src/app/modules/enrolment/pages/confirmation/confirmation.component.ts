import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as EnrolmentStatusReasonEnum } from '@shared/enums/enrolment-status-reason.enum';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit {
  public busy: Subscription;
  public isAutomatic: boolean;

  constructor(
    private enrolmentService: EnrolmentService
  ) { }

  public ngOnInit() {
    this.busy = this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) => {
        // Only automatic if the enrolment reason is `Automatic`
        this.isAutomatic = enrolment.currentStatus.enrolmentStatusReasons
          .every((reason: EnrolmentStatusReason) => reason.statusReasonCode === EnrolmentStatusReasonEnum.AUTOMATIC);
      });
  }
}
