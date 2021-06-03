import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  public banner: Banner;
  public locationCode: BannerLocationCode;

  constructor(
    private route: ActivatedRoute,
    private bannerResource: BannerResourceService,
  ) {
    this.locationCode = route.snapshot.data.locationCode;

  }

  public get type() {
    return (this.banner)
      ? BannerType[this.banner.bannerType]?.toLowerCase()
      : null;
  }

  public ngOnInit(): void {
    this.getBanner();
  }

  private getBanner(): void {
    if (!this.locationCode) {
      return;
    }

    this.bannerResource.getActiveBannerByLocationCode(this.locationCode)
      .subscribe((banner: Banner) => this.banner = banner);
  }
}
