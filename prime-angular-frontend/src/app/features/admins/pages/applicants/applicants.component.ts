import { Component, OnInit, SimpleChanges, Inject } from '@angular/core';

import { PrimeAPIService } from 'src/app/core/services/primeapi.service';
import { ToastService } from 'src/app/core/services/toast.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';

@Component({
  selector: 'app-applicants',
  templateUrl: './applicants.component.html',
  styleUrls: ['./applicants.component.scss']
})
export class ApplicantsComponent implements OnInit {
  // TODO: add models
  public applications;
  public sortedApplications;
  public showPending: boolean;

  constructor(
    private toastService: ToastService,
    private primeAPIService: PrimeAPIService,
    public dialog: MatDialog
  ) {
    this.showPending = true;
  }

  public showApplications(show: SimpleChanges) {
    (show.checked) ? this.getPending() : this.getAll();
  }

  private getAll() {
    this.showPending = false;
    this.sortedApplications = [...this.applications];
  }

  private getPending() {
    this.showPending = true;
    this.sortedApplications = this.applications.filter((app: any) => !app.approved);
  }

  public approveApplication(application) {
    application.approved = true;
    application.approvedDate = new Date();
    application.approvedReason = 'Approved by administrator.';

    this.primeAPIService.updateApplication(application)
      .subscribe(() =>
        this.toastService.openSuccessToast('Pharmacist application has been approved.')
      );
  }

  public removeApplication(applicationId: number) {
    // TODO: setup API endpoint to remove an application
    this.openDialog(applicationId);
  }

  public openDialog(applicationId: number): void {
    const dialogRef = this.dialog.open(DialogDeleteEnrolment, {
      width: '450px'
    });

    dialogRef.afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.sortedApplications = this.sortedApplications
            .filter((app: any) => app.id !== applicationId);
        }
      });
  }

  public ngOnInit() {
    this.primeAPIService.getApplications()
      .subscribe((data: any[]) => {
        this.applications = data.sort((a: any, b: any) => {

          if (a.approved < b.approved) {
            return -1;
          } else if (a.approved > b.approved) {
            return 1;
          }

          return 0;
        });

        this.getPending();
      });
  }
}

@Component({
  selector: 'app-modal-delete',
  templateUrl: 'modal-delete.html',
})
export class DialogDeleteEnrolment {

  constructor(
    public dialogRef: MatDialogRef<DialogDeleteEnrolment>
  ) { }

  public delete(shouldDelete: boolean): void {
    this.dialogRef.close(shouldDelete);
  }
}
