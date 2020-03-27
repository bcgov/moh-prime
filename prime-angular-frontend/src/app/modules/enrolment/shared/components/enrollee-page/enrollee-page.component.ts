import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription, EMPTY } from 'rxjs';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { FeedbackComponent, Feedback } from '@shared/components/dialogs/content/feedback/feedback.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { exhaustMap } from 'rxjs/operators';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { FeedbackResourceService } from '@shared/services/feedback-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-enrollee-page',
  templateUrl: './enrollee-page.component.html',
  styleUrls: ['./enrollee-page.component.scss']
})
export class EnrolleePageComponent implements OnInit {
  @Input() public busy: Subscription;
  @Input() public mode: 'default' | 'full';

  constructor(
    private dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected feedbackResource: FeedbackResourceService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected router: Router,
  ) {
    this.mode = 'default';
  }

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
            if (result.output.satisfied === undefined && result.output.comment === undefined) {
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
      .subscribe(
        (feedback: Feedback) => {
          this.toastService.openSuccessToast('Feedback has been recieved');
        },
        (error: any) => {
          this.toastService.openErrorToast('Feedback could not be recieved');
          this.logger.error('[Enrolment] Enrolments::feedback error has occurred: ', error);
        }
      );
  }

  public ngOnInit() { }

}
