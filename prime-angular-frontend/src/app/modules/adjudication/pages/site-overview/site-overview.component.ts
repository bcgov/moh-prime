import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationContainerComponent } from '@adjudication/shared/components/site-registration-container/site-registration-container.component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, EventEmitter, Inject, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-site-overview',
  templateUrl: './site-overview.component.html',
  styleUrls: ['./site-overview.component.scss']
})
export class SiteOverviewComponent extends SiteRegistrationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public actions: TemplateRef<any>;
  @Input() public content: TemplateRef<any>;
  @Input() public refresh: Observable<boolean>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    protected adjudicationResource: AdjudicationResource,
    protected organizationResource: OrganizationResource,
    protected siteResource: SiteResource,
    authService: AuthService,
    dialog: MatDialog,
    utilsService: UtilsService,
    toastService: ToastService,
  ) {
    super(defaultOptions,
      route,
      router,
      authService,
      organizationResource,
      siteResource,
      utilsService,
      dialog);

    this.hasActions = true;
  }

}
