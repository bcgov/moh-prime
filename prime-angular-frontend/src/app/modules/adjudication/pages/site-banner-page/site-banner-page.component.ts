import { Component, OnInit } from '@angular/core';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

@Component({
  selector: 'app-site-banner-page',
  templateUrl: './site-banner-page.component.html',
  styleUrls: ['./site-banner-page.component.scss']
})
export class SiteBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;

  constructor() {
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
  }

  ngOnInit(): void { }
}
