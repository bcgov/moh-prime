import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

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
    this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) => {
        this.isAutomatic = (!enrolment.currentStatus.enrolmentStatusReasons.length) ? true : false;
      });
  }
}
