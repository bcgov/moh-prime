import { Component, OnInit, Input } from '@angular/core';

import { Enrolment } from '@shared/models/enrolment.model';
import { IDialogContent } from '../../dialog-content.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';

@Component({
  selector: 'app-enrolment-status-reasons',
  templateUrl: './enrolment-status-reasons.component.html',
  styleUrls: ['./enrolment-status-reasons.component.scss']
})
export class EnrolmentStatusReasonsComponent implements OnInit, IDialogContent {
  public enrolment: Enrolment;

  constructor() { }

  @Input() public set data({ enrolment }: { enrolment: Enrolment }) {
    this.enrolment = enrolment;
  }

  public get enrolmentStatusReasons(): EnrolmentStatusReason[] {
    return this.enrolment.currentStatus.enrolmentStatusReasons;
  }

  public ngOnInit() { }
}
