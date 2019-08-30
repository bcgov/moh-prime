import { Component, OnInit, SimpleChanges, Inject } from '@angular/core';

import { PrimeAPIService } from 'src/app/core/services/primeapi.service';
import { ToastService } from 'src/app/core/services/toast.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';

@Component({
  selector: 'app-modal-delete',
  templateUrl: 'modal-delete.html',
})
export class DialogDeleteEnrolmentComponent {

  constructor(
    public dialogRef: MatDialogRef<DialogDeleteEnrolmentComponent>
  ) { }

  public delete(shouldDelete: boolean): void {
    this.dialogRef.close(shouldDelete);
  }
}


@Component({
  selector: 'app-applicants',
  templateUrl: './applicants.component.html',
  styleUrls: ['./applicants.component.scss']
})
export class ApplicantsComponent implements OnInit {
  // TODO: add models
  public applications;

  constructor(
    private toastService: ToastService,
    private primeAPIService: PrimeAPIService,
    public dialog: MatDialog
  ) { }

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
    const dialogRef = this.dialog.open(DialogDeleteEnrolmentComponent, {
      width: '450px'
    });

    dialogRef.afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.applications = this.applications
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
      });
  }
}
