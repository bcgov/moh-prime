import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FeedbackComponent, Feedback } from '@shared/components/dialogs/content/feedback/feedback.component';
import { FeedbackResourceService } from '@shared/services/feedback-resource.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { DialogContentOutput } from '@shared/components/dialogs/dialog-output.model';

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
      title: 'Feedback',
      message: 'Are you satisfied with the information provided to you?',
      actionText: 'Submit',
      component: FeedbackComponent
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        map((result: DialogContentOutput<Feedback>) => result?.output),
        exhaustMap((feedback: Feedback) => {
          if (feedback && Object.keys(feedback).length) {
            const enrolleeId = this.enrolmentService.enrolment.id;
            const route = this.router.url;
            feedback = { ...feedback, enrolleeId, route };
            return this.feedbackResource.createFeedback(feedback);
          }

          this.toastService.openSuccessToast('No feedback was provided');
          return EMPTY;
        })
      ).subscribe();
  }

  public ngOnInit(): void { }
}
