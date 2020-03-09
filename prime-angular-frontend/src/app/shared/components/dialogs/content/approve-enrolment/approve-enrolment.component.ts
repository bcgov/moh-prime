import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatCheckboxChange } from '@angular/material';

import { IDialogContent } from '@shared/components/dialogs/dialog-content.model';
import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-approve-enrolment',
  templateUrl: './approve-enrolment.component.html',
  styleUrls: ['./approve-enrolment.component.scss']
})
export class ApproveEnrolmentComponent implements OnInit, IDialogContent {
  @Output() output = new EventEmitter<boolean>();

  public enrolment: Enrolment;

  constructor() { }

  @Input()
  public set data({ enrolment }: { enrolment: Enrolment }) {
    this.enrolment = enrolment;
  }

  public get alwaysManual(): boolean {
    return this.enrolment ? this.enrolment.alwaysManual : false;
  }

  public onChange($event: MatCheckboxChange) {
    this.output.emit($event.checked);
  }

  public ngOnInit() { }
}
