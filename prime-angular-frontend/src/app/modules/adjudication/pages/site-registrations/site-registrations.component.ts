import { Component, OnInit, Input, TemplateRef, Output, EventEmitter } from '@angular/core';
import { Subscription, of, noop, EMPTY, Observable } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { MatDialog } from '@angular/material/dialog';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { Site } from '@registration/shared/models/site.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { exhaustMap, map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-site-registrations',
  templateUrl: './site-registrations.component.html',
  styleUrls: ['./site-registrations.component.scss']
})
export class SiteRegistrationsComponent extends AbstractComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public content: TemplateRef<any>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<Site>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private dialog: MatDialog
  ) {
    super(route, router);

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.columns = ['uniqueId', 'name', 'appliedDate', 'status', 'approvedDate', 'adjudicator', 'actions'];
    this.dataSource = new MatTableDataSource<Site>([]);

    this.showSearchFilter = false;
    this.baseRoutePath = [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.SITE_REGISTRATIONS];
  }

  public onSearch(search: string | null): void {
    this.setQueryParams({ search });
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  public onDelete(siteId: number) {
    const data: DialogOptions = {
      title: 'Delete Site',
      message: 'Are you sure you want to delete this site registration?',
      actionType: 'warn',
      actionText: 'Delete Site'
    };

    if (this.authService.isSuperAdmin()) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) => {
            if (result) {
              return of(noop);
            }
            return EMPTY;
          }),
          exhaustMap(() => this.adjudicationResource.deleteSite(siteId)),
        )
        .subscribe((site: Site) => this.getDataset(this.route.snapshot.queryParams));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeWithin(routePath);
  }

  public ngOnInit(): void {
    // Use existing query params for initial search
    this.getDataset(this.route.snapshot.queryParams);

    // Update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(queryParams));
  }

  private getDataset(queryParams: { search?: string, status?: number }) {
    const siteId = this.route.snapshot.params.id;
    const results$ = (siteId)
      ? this.getSiteById(siteId)
      : this.getSites(queryParams);

    this.busy = results$
      .subscribe((sites: Site[]) => this.dataSource.data = sites);
  }

  private getSiteById(siteId: number): Observable<Site[]> {
    return this.adjudicationResource
      .getSiteById(siteId)
      .pipe(
        map((site: Site) => [site]),
        tap(() => this.showSearchFilter = false)
      );
  }

  private getSites({ search, status }: { search?: string, status?: number }) {
    return this.adjudicationResource.getSites(search, status)
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private setQueryParams(queryParams: { search?: string, status?: number } = { search: null, status: null }) {
    // Passing `null` removes the query parameter from the URL
    queryParams = { ...this.route.snapshot.queryParams, ...queryParams };
    this.router.navigate([], { queryParams });
  }

}
