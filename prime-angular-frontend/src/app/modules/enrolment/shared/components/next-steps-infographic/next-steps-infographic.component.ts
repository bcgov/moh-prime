import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { Role } from '@auth/shared/enum/role.enum';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ImageComponent } from '@shared/components/dialogs/content/image/image.component';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-next-steps-infographic',
  templateUrl: './next-steps-infographic.component.html',
  styleUrls: ['./next-steps-infographic.component.scss']
})
export class NextStepsInfographicComponent implements OnInit {
  @Input() public enrolment: Enrolment;

  public busy: Subscription;
  public Role = Role;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private dialog: MatDialog
  ) { }

  public openQR(event: Event) {
    event.preventDefault();

    this.enrolmentResource.getQrCode(this.enrolment.id)
      .subscribe((qrCode: string) => {
        var data = qrCode
          ? { base64Image: qrCode }
          : null;
        var message = qrCode
          ? 'Scan this QR code to receive an invitation to your verifiable credential that can be stored in your digital wallet.'
          : 'No credential invitation found.';

        const options: DialogOptions = {
          title: 'Verified Credential',
          message,
          actionHide: true,
          cancelText: 'Close',
          data,
          component: ImageComponent
        };

        this.busy = this.dialog.open(ConfirmDialogComponent, { data: options })
          .afterClosed()
          .subscribe();
      });
  }

  public ngOnInit() { }
}
