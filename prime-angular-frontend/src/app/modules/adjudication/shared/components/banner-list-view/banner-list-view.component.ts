import { Component, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import moment from 'moment';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner, BannerViewModel } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-banner-list-view',
  templateUrl: './banner-list-view.component.html',
  styleUrls: ['./banner-list-view.component.scss']
})
export class BannerListViewComponent implements OnInit {
  @Input() public banners: Banner[];

  private routeUtils: RouteUtils;
  private dateFormatString: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private bannerResource: BannerResourceService,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
    this.dateFormatString = 'MMMM Do YYYY, HH:mm';
  }

  public onView(bannerId: number): void {
    this.routeUtils.routeRelativeTo([bannerId]);
  }

  public onCreate(): void {
    this.routeUtils.routeRelativeTo([0]);
  }

  public onRemove(bannerId: number): void {
    const data: DialogOptions = {
      title: 'Delete Banner',
      message: `Are you sure you want to delete this banner?`,
      actionText: 'Delete Banner'
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.bannerResource.deleteBanner(bannerId)
            .subscribe(() => this.banners = this.banners.filter(b => b.id !== bannerId));
        }
      });
  }

  public isExpired(banner: Banner): boolean {
    const viewModel = BannerViewModel.fromBanner(banner);
    return viewModel.endTimestamp.isBefore(moment());
  }

  public getTemplateProperties(banner: Banner) {
    const viewModel = BannerViewModel.fromBanner(banner);
    return [
      {
        key: 'Banner Type',
        value: BannerType[banner.bannerType]
      },
      {
        key: 'Start',
        value: viewModel.startTimestamp?.format(this.dateFormatString)
      },
      {
        key: 'End',
        value: viewModel.endTimestamp?.format(this.dateFormatString)
      }
    ];
  }

  public ngOnInit(): void { }
}
