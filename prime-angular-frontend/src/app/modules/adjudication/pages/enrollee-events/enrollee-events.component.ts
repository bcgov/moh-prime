import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, EMPTY, noop, of } from 'rxjs';

import { HttpEnrollee } from '@shared/models/enrolment.model';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { exhaustMap } from 'rxjs/operators';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';
import { MatDialog } from '@angular/material';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-enrollee-events',
  templateUrl: './enrollee-events.component.html',
  styleUrls: ['./enrollee-events.component.scss']
})
export class EnrolleeEventsComponent implements OnInit {
  public hasActions: boolean;

  constructor() {
    this.hasActions = true;
  }

  public ngOnInit() { }
}
