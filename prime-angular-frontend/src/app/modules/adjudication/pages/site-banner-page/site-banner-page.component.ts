import { Component, OnInit } from '@angular/core';

import { Subscription } from 'rxjs';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';

@Component({
  selector: 'app-site-banner-page',
  templateUrl: './site-banner-page.component.html',
  styleUrls: ['./site-banner-page.component.scss']
})
export class SiteBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;
  public banner: Banner;

  public busy: Subscription;

  constructor(
    private bannerResource: BannerResourceService,
  ) {
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
  }

  public onSave(banner: Banner): void {
    this.busy = this.bannerResource.createOrUpdateSiteLandingBanner(banner)
      .subscribe((newOrUpdatedBanner: Banner) => this.banner = newOrUpdatedBanner);
  }

  public onDelete(): void {
    this.busy = this.bannerResource.deleteSiteLandingBanner()
      .subscribe(() => this.banner = null);
  }

  public ngOnInit(): void {
    this.getBanner();
  }

  private getBanner(): void {
    this.busy = this.bannerResource.getSiteLandingBanner()
      .subscribe((banner: Banner) => this.banner = banner);
  }
}
