import { Component, OnInit } from '@angular/core';
import { AccessTerm } from '@shared/models/access-term.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { Subscription } from 'rxjs';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-access-term',
  templateUrl: './enrollee-access-term.component.html',
  styleUrls: ['./enrollee-access-term.component.scss']
})
export class EnrolleeAccessTermComponent implements OnInit {
  public busy: Subscription;
  public accessTerm: AccessTerm;

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource,
    private logger: LoggerService,
    private toastService: ToastService,
  ) { }

  public routeTo() {
    this.router.navigate(
      ['../'],
      { relativeTo: this.route.parent }
    );
  }

  public ngOnInit() {
    this.getAccessTerm();
  }

  private getAccessTerm() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource.getAccessTerm(enrolleeId, accessTermId)
      .subscribe(
        (accessTerm: AccessTerm) => {
          this.logger.info('ACCESS TERM', accessTerm);
          this.accessTerm = accessTerm;
        },
        (error: any) => {
          this.toastService.openErrorToast('Access Term could not be retrieved');
          this.logger.error('[ADJUDICATION] EnrolleeAccessTermHistory::getAccessTerm error has occurred: ', error);
        }
      );
  }
}
