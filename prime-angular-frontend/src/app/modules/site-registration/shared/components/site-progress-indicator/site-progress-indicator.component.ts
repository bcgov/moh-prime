import { Component, OnInit, Input } from '@angular/core';
import { SiteRoutes } from 'app/modules/site-registration/site-registration.routes';

@Component({
  selector: 'app-site-progress-indicator',
  templateUrl: './site-progress-indicator.component.html',
  styleUrls: ['./site-progress-indicator.component.scss']
})
export class SiteProgressIndicatorComponent implements OnInit {
  @Input() public currentRoute: SiteRoutes;

  constructor() { }

  ngOnInit() {
  }

}
