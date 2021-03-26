import { Component, Input, OnInit } from '@angular/core';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner } from '@shared/models/banner.model';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {
  @Input() public banner: Banner;

  constructor() { }

  public getType() {
    return (this.banner) 
      ? BannerType[this.banner.bannerType]?.toLowerCase() 
      : null;
  }

  public ngOnInit(): void { }
}
