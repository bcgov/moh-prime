import { Component, Input, OnInit } from '@angular/core';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';

@Component({
  selector: 'app-banner-wrapper',
  templateUrl: './banner-wrapper.component.html',
  styleUrls: ['./banner-wrapper.component.scss']
})
export class BannerWrapperComponent implements OnInit {
  @Input() public locationCode: BannerLocationCode;
  public banner: Banner;

  constructor(
    private bannerResource: BannerResourceService,
  ) { }

  ngOnInit(): void {
    this.getBanner();
  }

  private getBanner(): void {
    this.bannerResource.getActiveBannerByLocationCode(this.locationCode)
      .subscribe((banner: Banner) => this.banner = banner);
  }

}
