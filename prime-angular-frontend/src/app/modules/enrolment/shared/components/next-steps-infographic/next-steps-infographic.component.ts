import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';

import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-next-steps-infographic',
  templateUrl: './next-steps-infographic.component.html',
  styleUrls: ['./next-steps-infographic.component.scss']
})
export class NextStepsInfographicComponent implements OnInit {
  @Input() public enrolment: Enrolment;

  public busy: Subscription;

  constructor(
    private dialog: MatDialog,
    private authService: AuthService
  ) { }

  public get hasVCIssuance(): boolean {
    return this.authService.hasVCIssuance();
  }

  public openQR(event: Event) {
    event.preventDefault();

    const data: DialogOptions = {
      title: 'Verified Credential',
      message: 'Scan this QR code to receive an invitation to your verifiable credential that can be stored in your digital wallet.',
      actionHide: true,
      cancelText: 'Close',
      data: { base64Image: this.enrolment.base64QRCode },
      component: ImageComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe()
      .subscribe();
  }

  public ngOnInit() { }
}
