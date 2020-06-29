import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { HttpEnrollee, Enrolment } from '@shared/models/enrolment.model';
import { Subscription } from 'rxjs';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';

@Component({
  selector: 'app-next-steps-infographic',
  templateUrl: './next-steps-infographic.component.html',
  styleUrls: ['./next-steps-infographic.component.scss']
})
export class NextStepsInfographicComponent implements OnInit {
  @Input() public enrolment: Enrolment;

  public busy: Subscription;

  constructor(private dialog: MatDialog) { }

  public openQR() {
    const data: DialogOptions = {
      title: 'Verified Credential',
      message: 'This QR code is your verified credential, you can now store it in your digital wallet.',
      actionHide: true,
      cancelText: 'PRIME',
      data: { base64QRCode: this.enrolment.base64QRCode },
      component: ImageComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe()
      .subscribe();
  }

  public ngOnInit() { }
}
