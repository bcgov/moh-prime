import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { IDialogContent } from '@shared/components/dialogs/dialog-content.model';
import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-approve-enrolment',
  templateUrl: './approve-enrolment.component.html',
  styleUrls: ['./approve-enrolment.component.scss']
})
export class ApproveEnrolmentComponent implements OnInit, IDialogContent {
  @Output() public output = new EventEmitter<boolean>();

  public enrolment: Enrolment;

  constructor() { }

  @Input()
  public set data({ enrolment }: { enrolment: Enrolment }) {
    this.enrolment = enrolment;
  }

  public get alwaysManual(): boolean {
    return this.enrolment ? this.enrolment.alwaysManual : false;
  }

  public onChange(change: MatCheckboxChange) {
    this.output.emit(change.checked);
  }

  public ngOnInit() { }
}
