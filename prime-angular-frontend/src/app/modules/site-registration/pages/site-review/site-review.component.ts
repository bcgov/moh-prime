import { Component, OnInit } from '@angular/core';
import { Subscription, EMPTY } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { exhaustMap } from 'rxjs/operators';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-site-review',
  templateUrl: './site-review.component.html',
  styleUrls: ['./site-review.component.scss']
})
export class SiteReviewComponent implements OnInit {
  public busy: Subscription;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public onSubmit() {
    this.router.navigate([SiteRoutes.CONFIRMATION], { relativeTo: this.route.parent });
  }

  public onRoute(routePath: EnrolmentRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  ngOnInit() {
  }



}
