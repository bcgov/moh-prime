import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FeedbackComponent, Feedback } from '@shared/components/dialogs/content/feedback/feedback.component';
import { FeedbackResourceService } from '@shared/services/feedback-resource.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-notification-confirmation',
  templateUrl: './notification-confirmation.component.html',
  styleUrls: ['./notification-confirmation.component.scss']
})
export class NotificationConfirmationComponent implements OnInit {
  public busy: Subscription;
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private router: Router,
    private enrolmentService: EnrolmentService,
    private feedbackResource: FeedbackResourceService,
    private toastService: ToastService,
    private dialog: MatDialog
  ) { }

  public feedback() {
    const data: DialogOptions = {
      title: 'Feedback?',
      message: 'Are you satisfied with the information provided to you?',
      actionText: 'Submit',
      component: FeedbackComponent
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: Feedback }) => {
          if (result) {
            if (result.output?.satisfied && result.output?.comment) {
              this.toastService.openSuccessToast('No Feedback entered.');
              return EMPTY;
            }
            result.output.enrolleeId = this.enrolmentService.enrolment.id;
            result.output.route = this.router.url;
            return this.feedbackResource.createFeedback(result.output);
          }
          return EMPTY;
        }),
      )
      .subscribe();
  }

  public ngOnInit(): void { }
}
