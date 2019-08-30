import { Component, OnInit, SimpleChanges } from '@angular/core';

import { PrimeAPIService } from 'src/app/core/services/primeapi.service';
import { ToastService } from 'src/app/core/services/toast.service';

@Component({
  selector: 'app-applicants',
  templateUrl: './applicants.component.html',
  styleUrls: ['./applicants.component.scss']
})
export class ApplicantsComponent implements OnInit {
  // TODO: add models
  public applications;
  public sortedApplications;
  public isPending: boolean;

  constructor(
    private toastService: ToastService,
    private primeAPIService: PrimeAPIService
  ) { }

  public showLabel() {
    return (this.isPending) ? 'Review Pending' : 'Review All';
  }

  public showPending(show: SimpleChanges) {
    (show.checked) ? this.getPending() : this.getAll();
  }

  private getAll() {
    this.isPending = false;
    this.sortedApplications = [...this.applications];
  }

  private getPending() {
    this.isPending = true;
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
  }

  public ngOnInit() {
    this.primeAPIService.getApplications()
      .subscribe((data: any[]) => {
        console.log('APPLICATIONS', this.applications);

        this.applications = data.sort((a: any, b: any) => {

          if (a.approved < b.approved) {
            return -1;
          } else if (a.approved > b.approved) {
            return 1;
          }

          return 0;
        });

        console.log('APPLICATIONS', this.applications);

        this.getPending();
      });
  }
}
