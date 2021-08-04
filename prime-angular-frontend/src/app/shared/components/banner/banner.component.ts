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
  public banners: Banner[];
  public locationCode: BannerLocationCode;

  constructor(
    private route: ActivatedRoute,
    private bannerResource: BannerResourceService,
  ) {
    this.locationCode = route.snapshot.data.locationCode;

  }

  public type(banner: Banner) {
    return (banner)
      ? BannerType[banner.bannerType]?.toLowerCase()
      : null;
  }

  public ngOnInit(): void {
    this.getBanners();
  }

  private getBanners(): void {
    if (!this.locationCode) {
      return;
    }

    this.bannerResource.getActiveBannersByLocationCode(this.locationCode)
      .subscribe((banners: Banner[]) => this.banners = this.sortByUrgency(banners));
  }

  private sortByUrgency(banners: Banner[]): Banner[] {
    return banners.sort((a: Banner, b: Banner) => b.bannerType - a.bannerType);
  }
}
