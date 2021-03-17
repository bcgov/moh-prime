import { Component, OnInit } from '@angular/core';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

@Component({
  selector: 'app-enrollee-banner-page',
  templateUrl: './enrollee-banner-page.component.html',
  styleUrls: ['./enrollee-banner-page.component.scss']
})
export class EnrolleeBannerPageComponent implements OnInit {
  public locationCode: BannerLocationCode;

  constructor(
  ) {
    this.locationCode = BannerLocationCode.ENROLMENT_LANDING_PAGE;
  }

  ngOnInit(): void { }

}
