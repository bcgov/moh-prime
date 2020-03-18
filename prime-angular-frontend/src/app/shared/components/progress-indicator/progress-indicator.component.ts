import { Component, OnInit, Input } from '@angular/core';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-progress-indicator',
  templateUrl: './progress-indicator.component.html',
  styleUrls: ['./progress-indicator.component.scss']
})
export class ProgressIndicatorComponent implements OnInit {
  @Input() public inProgress: boolean;
  @Input() public currentRoute: any;
  @Input() public message: string;
  @Input() public routes: any;

  public percentComplete: number;

  constructor() {
    this.routes = EnrolmentRoutes.initialEnrolmentRouteOrder();
  }

  public ngOnInit() {
    const currentRoute = this.routes.findIndex(er => er === this.currentRoute);
    const currentPage = (currentRoute > -1) ? currentRoute : 0;
    const totalPages = this.routes.length - 1;

    const percentComplete = Math.trunc(currentPage / totalPages * 100);

    this.percentComplete = (this.inProgress)
      ? percentComplete
      : 100;
  }
}
