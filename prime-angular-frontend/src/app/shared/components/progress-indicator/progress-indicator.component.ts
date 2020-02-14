import { Component, OnInit, Input } from '@angular/core';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-progress-indicator',
  templateUrl: './progress-indicator.component.html',
  styleUrls: ['./progress-indicator.component.scss']
})
export class ProgressIndicatorComponent implements OnInit {
  @Input() public inProgress: boolean;
  @Input() public currentRoute: EnrolmentRoutes;
  @Input() public message: string;

  public percentComplete: number;

  constructor() { }

  public ngOnInit() {
    const enrolmentRoutes = EnrolmentRoutes.initialEnrolmentRouteOrder();
    const currentRoute = enrolmentRoutes.findIndex(er => er === this.currentRoute);
    const currentPage = (currentRoute > -1) ? currentRoute : 0;
    const totalPages = enrolmentRoutes.length - 1;

    const percentComplete = Math.trunc(currentPage / totalPages * 100);

    this.percentComplete = (this.inProgress)
      ? percentComplete
      : 100;
  }
}
