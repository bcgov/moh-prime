import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { LicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-license-classes-maintenance-page',
  templateUrl: './license-classes-maintenance-page.component.html',
  styleUrls: ['./license-classes-maintenance-page.component.scss']
})
export class LicenseClassesMaintenancePageComponent implements OnInit {

  public dataSource: MatTableDataSource<LicenseConfig>;
  public columns: string[];

  constructor(
    private configService: ConfigService
  ) {
    this.columns = [
      '',
      'code',
      // 'licenseClass',
      // 'pracRefId',
      // 'nimr',
      // 'lpc',
      // 'forceManual',
      // 'validate',
      // 'practiceId'
    ];
    this.dataSource = new MatTableDataSource<LicenseConfig>([]);
  }

  ngOnInit(): void {
    console.log(this.configService.licenses);
    this.dataSource.data = this.configService.licenses;
  }

}
