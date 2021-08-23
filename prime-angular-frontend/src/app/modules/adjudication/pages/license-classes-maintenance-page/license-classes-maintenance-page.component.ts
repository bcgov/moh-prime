import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { CollegeConfig, CollegeLicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

export class LicenseMaintenanceConfig {
  collegeName: string;
  licenseCode: number;
  prefix: string;
  namedInImReg?: boolean;
  licensedToProvideCare?: boolean;
  manual?: boolean;
  validate?: boolean;
  prescriberIdType?: PrescriberIdTypeEnum;
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
    private router: Router
  ) {
    this.columns = [
      'collegeName',
      'licenseCode',
      'prefix',
      'namedInImReg',
      'licensedToProvideCare',
      'manual',
      'validate',
      'prescriberIdType'
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
            ...license
          } as LicenseMaintenanceConfig;
        })
        : { collegeName: college.name } as LicenseMaintenanceConfig;
    });

    this.dataSource.data = licenceMaintenanceConfig;
  }

}
