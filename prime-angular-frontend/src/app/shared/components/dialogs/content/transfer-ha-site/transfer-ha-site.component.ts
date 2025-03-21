import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';

@Component({
  selector: 'app-transfer-ha-site',
  templateUrl: './transfer-ha-site.component.html',
  styleUrls: ['./transfer-ha-site.component.scss']
})
export class TransferHASiteComponent implements OnInit {
  @Output() public transferSite: EventEmitter<boolean>;
  public siteCount: number;
  public currentAuthorizedUserName: string;
  public currentAuthorizedUserId: number;
  public healthAuthorityId: number;

  public form: UntypedFormGroup;
  public transferSiteClick: boolean;
  public authorizedUsers: AuthorizedUser[];

  constructor(
    private healthAuthorityResource: HealthAuthorityResource,
    private healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteCount = data.data.siteCount;
    this.currentAuthorizedUserName = data.data.currentAuthorizedUserName;
    this.currentAuthorizedUserId = data.data.currentAuthorizedUserId;
    this.healthAuthorityId = data.data.healthAuthorityId;

    this.transferSite = new EventEmitter<boolean>();
  }

  public get authorizedUser(): UntypedFormControl {
    return this.form.get('authorizedUser') as UntypedFormControl;
  }

  public onCancel() {
    this.dialogRef.close();
    this.transferSite.emit(false);
  }

  public onTransferSite() {
    if (this.form.valid) {
      this.transferSiteClick = true;

      this.healthAuthoritySiteResource.transferHealthAuthoritySite(this.healthAuthorityId, this.currentAuthorizedUserId, this.authorizedUser.value)
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
          this.transferSite.emit(true);
        });
    }
    this.authorizedUser.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getValidAuthorizedUsers();
    // To accommodate lengthy instruction text
    this.dialogRef.updateSize('750px', '50%');
    this.transferSiteClick = false;
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      authorizedUser: [null, [Validators.required]],
    });
  }

  private getValidAuthorizedUsers(): void {
    this.healthAuthorityResource.getAuthorizedUsersByHealthAuthority(this.healthAuthorityId)
      .subscribe((result) => this.authorizedUsers = result.filter(au => au.id != this.currentAuthorizedUserId &&
        (au.status === AccessStatusEnum.APPROVED || au.status === AccessStatusEnum.ACTIVE)));
  }
}
