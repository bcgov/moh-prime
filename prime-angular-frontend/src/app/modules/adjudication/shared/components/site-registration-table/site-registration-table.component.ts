import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-site-registration-table',
  templateUrl: './site-registration-table.component.html',
  styleUrls: ['./site-registration-table.component.scss']
})
export class SiteRegistrationTableComponent implements OnInit {
  @Input() public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;
  @Input() public careSettingFilter$: Observable<CareSettingEnum> = of(0);
  @Output() public assign: EventEmitter<number>;
  @Output() public reassign: EventEmitter<number>;
  @Output() public notify: EventEmitter<number>;
  @Output() public reload: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  private originalColumns: string[];
  public columns: string[];

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
      'authorizedUser',
      'siteDoingBusinessAs',
      'submissionDate',
      'assignedTo',
      'state',
      'siteId',
      'remoteUsers',
      'missingBusinessLicence',
      'actions'
    ];
    this.originalColumns = this.columns;
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
    return (status === SiteStatusType.APPROVED)
      ? 'Approved'
      : (status === SiteStatusType.DECLINED)
        ? 'Declined'
        : 'Under Review';
  }

  public remoteUsers(siteRegistration: SiteRegistrationListViewModel): number | 'Yes' | 'No' | 'N/A' {
    const count = siteRegistration.remoteUserCount;

    return (siteRegistration.careSettingCode !== CareSettingEnum.COMMUNITY_PHARMACIST)
      ? (!this.activatedRoute.snapshot.params.sid)
        ? (count) ? 'Yes' : 'No'
        : count
      : 'N/A';
  }

  public ngOnInit(): void {
    this.careSettingFilter$.subscribe((value) => {
      switch (value) {
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
          this.columns = this.originalColumns.filter(column => !['missingBusinessLicence', 'authorizedUser'].includes(column));
          break;
        case CareSettingEnum.COMMUNITY_PHARMACIST:
          this.columns = this.originalColumns.filter(column => !['remoteUsers', 'authorizedUser'].includes(column));
          break;
        case CareSettingEnum.HEALTH_AUTHORITY:
          this.columns = this.originalColumns.filter(column => !['missingBusinessLicence', 'signingAuthority'].includes(column));
          break;
        default:
          this.columns = this.originalColumns;
          break;
      }
    });
  }
}
