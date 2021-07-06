import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';


@Component({
  selector: 'app-demographic-overview',
  templateUrl: './demographic-overview.component.html',
  styleUrls: ['./demographic-overview.component.scss']
})
export class DemographicOverviewComponent implements OnInit {
  @Input() firstName = '';
  @Input() givenNames = '';
  @Input() lastName = '';
  @Input() dateOfBirth = '';
  @Input() physicalAddress = '';
  @Input() phone = '';
  @Input() phoneExtension = '';
  @Input() email = '';
  @Input() smsPhone = '';
  public routeUtils: RouteUtils;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }


  ngOnInit(): void {
  }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }

}
