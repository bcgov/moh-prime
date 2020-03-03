import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';
import { IDialogContent } from '../../dialog-content.model';
import { MatCheckboxChange } from '@angular/material';

@Component({
  selector: 'app-approve-enrolment',
  templateUrl: './approve-enrolment.component.html',
  styleUrls: ['./approve-enrolment.component.scss']
})
export class ApproveEnrolmentComponent implements OnInit, IDialogContent {

  public enrolment: Enrolment;
  @Output() output = new EventEmitter<{ output: boolean }>();

  constructor() { }

  @Input()
  public set data({ enrolment }: { enrolment: Enrolment }) {
    this.enrolment = enrolment;
  }

  public get alwaysManual(): boolean {
    return this.enrolment ? this.enrolment.alwaysManual : false;
  }

  public onChange($event: MatCheckboxChange) {
    this.output.emit({ output: $event.checked });
  }

  public ngOnInit() { }
}
