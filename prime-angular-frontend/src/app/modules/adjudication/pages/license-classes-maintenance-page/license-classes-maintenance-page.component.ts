import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { CollegeConfig, CollegeLicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';
export class LicenseMaintenanceConfig {
  collegeName: string;
  licenseCode: number;
  prefix: string;
  namedInImReg?: boolean;
  licensedToProvideCare?: boolean;
  manual?: boolean;
  validate?: boolean;
  prescriberIdType?: PrescriberIdTypeEnum;
};
@Component({
  selector: 'app-license-classes-maintenance-page',
  templateUrl: './license-classes-maintenance-page.component.html',
  styleUrls: ['./license-classes-maintenance-page.component.scss']
})
export class LicenseClassesMaintenancePageComponent implements OnInit {

  public dataSource: MatTableDataSource<LicenseMaintenanceConfig>;
  public columns: string[];

  PrescriberIdTypeEnum = PrescriberIdTypeEnum;

  constructor(
    private configService: ConfigService
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
  }

  ngOnInit(): void {
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
