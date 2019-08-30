import { Component, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material';

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

  constructor(
    private toastService: ToastService,
    private primeAPIService: PrimeAPIService
  ) { }

  public authenticate() {
    this.toastService.openSuccessToast('Pharmacist has been authenticated.');
  }

  public ngOnInit() {
    this.primeAPIService.getApplications()
      .subscribe(data => {
        this.applications = data;
      });
  }
}
