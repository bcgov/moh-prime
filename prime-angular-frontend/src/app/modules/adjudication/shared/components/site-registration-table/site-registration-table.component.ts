import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  @Input() public columns: string[];
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<number>;
  @Output() public reload: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public SiteStatusType = SiteStatusType;
  public CareSettingEnum = CareSettingEnum;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;

  constructor(
    private activatedRoute: ActivatedRoute,
  ) {
    this.columns = [
      'prefixes',
      'displayId',
      'organizationName',
      'signingAuthority',
      'siteDoingBusinessAs',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'careSetting',
      'missingBusinessLicence',
      'actions'
    ];
    this.assign = new EventEmitter<number>();
    this.reassign = new EventEmitter<number>();
    this.notify = new EventEmitter<number>();
    this.reload = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
  }

  public onAssign(siteId: number): void {
    this.assign.emit(siteId);
  }

  public onReassign(siteId: number): void {
    this.reassign.emit(siteId);
  }

  public onNotify(siteId: number): void {
    this.notify.emit(siteId);
  }

  public onReload(siteId: number): void {
    this.reload.emit(siteId);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  // TODO status lookup for sites would remove the need for this method and only require pipes
  public displayStatus(status: SiteStatusType) {
    switch (status) {
      case SiteStatusType.EDITABLE:
        return "Editable";
      case SiteStatusType.IN_REVIEW:
        return "In Review";
      case SiteStatusType.LOCKED:
        return "Locked";
      default:
        return "Editable";
    }
  }

  public remoteUsers(siteRegistration: SiteRegistrationListViewModel): number | 'Yes' | 'No' | 'N/A' {
    const count = siteRegistration.remoteUserCount;

    return (siteRegistration.careSettingCode !== CareSettingEnum.COMMUNITY_PHARMACIST)
      ? (!this.activatedRoute.snapshot.params.sid)
        ? (count) ? 'Yes' : 'No'
        : count
      : 'N/A';
  }

  public ngOnInit(): void { }
}
