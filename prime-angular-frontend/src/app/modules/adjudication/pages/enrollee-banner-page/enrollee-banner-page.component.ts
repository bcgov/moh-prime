import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';

@Component({
  selector: 'app-enrollee-banner-page',
  templateUrl: './enrollee-banner-page.component.html',
  styleUrls: ['./enrollee-banner-page.component.scss']
})
export class EnrolleeBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;
  public banner: Banner;

  public busy: Subscription;

  constructor(
    private bannerResource: BannerResourceService,
  ) {
    this.locationCode = BannerLocationCode.ENROLMENT_LANDING_PAGE;
  }

  public onSave(banner: Banner): void {
    this.busy = this.bannerResource.createOrUpdateEnrolmentLandingBanner(banner)
      .subscribe((newOrUpdatedBanner: Banner) => this.banner = newOrUpdatedBanner);
  }

  public onDelete(): void {
    this.busy = this.bannerResource.deleteEnrolmentLandingBanner()
      .subscribe(() => this.banner = null);
  }

  public ngOnInit(): void {
    this.getBanner();
  }

  private getBanner(): void {
    this.busy = this.bannerResource.getEnrolmentLandingBanner()
      .subscribe((banner: Banner) => this.banner = banner);
  }
}
