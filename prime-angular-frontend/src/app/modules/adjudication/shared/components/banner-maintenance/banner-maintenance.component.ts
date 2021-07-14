import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, ValidationErrors, Validators } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import moment from 'moment';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormGroupValidators } from '@lib/validators/form-group.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner } from '@shared/models/banner.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { BannerResourceService } from '@shared/services/banner-resource.service';
import { Role } from '@auth/shared/enum/role.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

import { BannerMaintenanceForm } from './banner-maintenance-form.model';
import { BannerMaintenanceFormState } from './banner-maintenance-form-state.class';

export class IsSameOrBeforeErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted || control?.touched);
    const requiredControl = (!!(control?.hasError('required')) && dirtyOrSubmitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('isSameOrBefore') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent || requiredControl);
  }
}

@Component({
  selector: 'app-banner-maintenance',
  templateUrl: './banner-maintenance.component.html',
  styleUrls: ['./banner-maintenance.component.scss']
})
export class BannerMaintenanceComponent extends AbstractEnrolmentPage implements OnInit {
  @Input() public locationCode: BannerLocationCode;
  @Input() public path: AdjudicationRoutes;

  @Output() public save: EventEmitter<Banner>;
  @Output() public delete: EventEmitter<null>;

  public formState: BannerMaintenanceFormState;

  public internalBanner: Banner;

  public isSameOrBeforeErrorStateMatcher: IsSameOrBeforeErrorStateMatcher;

  public hasActions: boolean;
  public editorConfig: Record<string, string>;

  public Role = Role;
  public BannerType = BannerType;
  public BannerLocationCode = BannerLocationCode;

  public readonly hoursTimePattern = {
    A: { pattern: /[0-2]/ },
    B: { pattern: /[0-9]/ },
    C: { pattern: /[0-5]/ }
  };

  private routeUtils: RouteUtils;

  constructor(
    protected formUtils: FormUtilsService,
    protected dialog: MatDialog,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private bannerResource: BannerResourceService,
    private location: Location,
    router: Router
  ) {
    super(dialog, formUtils);
    this.hasActions = false;
    this.editorConfig = {
      height: '18rem',
      base_url: '/tinymce',
      suffix: '.min',
      plugins: 'lists advlist',
      toolbar: 'undo redo | bold italic underline | bullist numlist outdent indent | removeformat',
      menubar: 'false'
    };
    this.save = new EventEmitter<Banner>();
    this.delete = new EventEmitter();
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.BANNERS);
  }

  public onDelete(): void {
    const bannerId = +this.route.snapshot.params.bid;
    if (!bannerId) {
      this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.BANNERS]);
    }

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
            .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.BANNERS]))
        }
      });
  }

  public onUpdate(event: { editor: any }): void {
    if (!event.editor) { return; }
    this.internalBanner = this.formState.json;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new BannerMaintenanceFormState(this.fb, this.formUtilsService, this.locationCode);

    this.formState.title.valueChanges.subscribe(() => this.internalBanner = this.formState.json);
    this.formState.bannerType.valueChanges.subscribe(() => this.internalBanner = this.formState.json);
    this.formState.bannerLocationCode.setValue(this.locationCode);
  }

  protected patchForm(): void {
    const bannerId = +this.route.snapshot.params.bid;
    if (!bannerId) {
      return;
    }

    this.bannerResource.getBanner(bannerId)
      .subscribe((banner: Banner) => {
        this.formState.patchValue(banner);
        this.internalBanner = this.formState.json;
      })
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    const payload = this.formState.json;
    const bannerId = +this.route.snapshot.params.bid;
    let request$ = this.bannerResource.updateBanner(bannerId, payload)
      .pipe(map(() => bannerId));

    if (!bannerId) {
      request$ = this.bannerResource.createBanner(this.locationCode, payload)
        .pipe(
          map((banner: Banner) => {
            // Replace the URL with redirection, and prevent initial
            // ID of zero being pushed onto browser history
            this.location.replaceState([AdjudicationRoutes.MODULE_PATH, this.path, AdjudicationRoutes.BANNERS, banner.id].join('/'));
            return banner.id;
          })
        );
    }

    return request$;
  }

  protected afterSubmitIsSuccessful(bannerId: number): void {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.BANNERS]);
  }
}
