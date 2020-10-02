import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { Feedback } from '@shared/models/feedback.model';
import { FeedbackResourceService } from '@core/resources/feedback-resource.service';
import { FeedbackComponent } from '@shared/components/dialogs/content/feedback/feedback.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogContentOutput } from '@shared/components/dialogs/dialog-output.model';

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
      title: 'Feedback',
      icon: 'feedback',
      message: 'How could we improve PRIME? If you could change something, what would it be?',
      actionText: 'Submit',
      actionLink: {
        href: 'https://prime245042.typeform.com/to/mUn5V0',
        text: 'Take 5 Minute Survey',
        target: '_blank'
      },
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
