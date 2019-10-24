import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
  public title: string;
  public message: string;
  public confirm: string;
  public cancel: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {
    this.title = 'Delete Enrolment';
    this.message = 'Are you sure you want to delete this enrolment?';
    this.confirm = 'Delete Enrolment';
    this.cancel = 'Cancel';
  }

  public ngOnInit() { }
}
