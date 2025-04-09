import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { CollegeConfig, CollegeLicenseConfig, IWeightedConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { UtilsService } from '@core/services/utils.service';

export class LicenseMaintenanceConfig implements IWeightedConfig {
  collegeName: string;
  licenseCode: number;
  prefix: string;
  namedInImReg?: boolean;
  licensedToProvideCare?: boolean;
  allowRequestRemoteAccess?: boolean;
  manual?: boolean;
  validate?: boolean;
  prescriberIdType?: PrescriberIdTypeEnum;
  weight: number;
  collegeLicenseGroupingCode: number;
  multijurisdictional?: boolean;
}

@Component({
  selector: 'app-license-classes-maintenance-page',
  templateUrl: './license-classes-maintenance-page.component.html',
  styleUrls: ['./license-classes-maintenance-page.component.scss']
})
export class LicenseClassesMaintenancePageComponent implements OnInit {
  public busy: Subscription;
  public dataSource: MatTableDataSource<LicenseMaintenanceConfig>;
  public columns: string[];
  PrescriberIdTypeEnum = PrescriberIdTypeEnum;
  private routeUtils: RouteUtils;

  constructor(
    private configService: ConfigService,
    protected route: ActivatedRoute,
    private router: Router,
    private utilsService: UtilsService
  ) {
    this.columns = [
      'collegeName',
      'licenseCode',
      'prefix',
      'namedInImReg',
      'licensedToProvideCare',
      'manual',
      'validate',
      'prescriberIdType',
      'allowRequestRemoteAccess',
      'multijurisdictional'
    ];
    this.dataSource = new MatTableDataSource<LicenseMaintenanceConfig>([]);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    const licenceMaintenanceConfig = this.configService.colleges?.flatMap((college: CollegeConfig) => {
      return college.collegeLicenses.length
        ? college.collegeLicenses.map((collegeLicense: CollegeLicenseConfig) => {
          const license = this.configService.licenses.find(l => l.code === collegeLicense.licenseCode);
          return {
            collegeName: college.name,
            licenseCode: collegeLicense.licenseCode,
            discontinued: collegeLicense.discontinued,
            collegeLicenseGroupingCode: collegeLicense.collegeLicenseGroupingCode,
            ...license
          } as LicenseMaintenanceConfig;
        }).sort((a: LicenseMaintenanceConfig, b: LicenseMaintenanceConfig) => a.weight - b.weight)
        : { collegeName: college.name } as LicenseMaintenanceConfig;
    });

    this.dataSource.data = licenceMaintenanceConfig;
  }

}
