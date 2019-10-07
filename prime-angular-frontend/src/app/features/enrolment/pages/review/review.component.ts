import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  // TODO: make a proper enrolment model
  public enrolment: any;

  constructor(
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private logger: LoggerService
  ) { }

  public declarationYesNo(declared: boolean) {
    return (declared) ? 'Yes' : 'No';
  }

  public route(route: string) {
    this.router.navigate(['enrolment', route]);
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentStateService.getEnrolment();

    this.logger.info('ENROLMENT', this.enrolment);
  }
}
