import { Component, Input, OnInit } from '@angular/core';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { HttpSite, SiteSubmission } from '@shared/models/site-submission.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-site-submission',
  templateUrl: './site-submission.component.html',
  styleUrls: ['./site-submission.component.scss']
})
export class SiteSubmissionComponent implements OnInit {
  @Input() public siteId: number;
  @Input() public siteSubmissionId: number;

  public submission: HttpSite;
  public CareSettingEnum = CareSettingEnum;

  constructor(
    private siteResource: SiteResource,
    private utilsService: UtilsService,
  ) { }

  ngOnInit(): void {
    this.siteResource.getSiteSubmission(this.siteId, this.siteSubmissionId)
      .subscribe((siteSubmission: SiteSubmission) => {
        this.submission = siteSubmission.profileSnapshot;
      })
  }

  public formatTime(time: string): string {
    if (time === "1.00:00:00") {
      return "24:00"
    } else {
      return time.split(":").slice(0, 2).join(":");
    }
  }

  public downloadBusinessLicenceDocument(siteId: number, businessLicenceId: number): void {
    this.siteResource.getBusinessLicenceDocumentToken(siteId, businessLicenceId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }
}
