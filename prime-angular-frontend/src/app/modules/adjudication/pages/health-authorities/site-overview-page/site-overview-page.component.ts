import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { asyncValidator } from '@lib/validators/form-async.validators';

import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';

@Component({
  selector: 'app-site-overview-page',
  templateUrl: './site-overview-page.component.html',
  styleUrls: ['./site-overview-page.component.scss']
})
export class SiteOverviewPageComponent implements OnInit {
  public busy: Subscription;
  public site: HealthAuthoritySiteAdmin;
  public form: FormGroup;
  public refresh: BehaviorSubject<boolean>;

  constructor(
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    private formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private siteResource: SiteResource,
  ) {
    this.refresh = new BehaviorSubject<boolean>(null);
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.busy = this.healthAuthorityResource
      .getHealthAuthorityAdminSite(+this.route.snapshot.params.haid, +this.route.snapshot.params.sid)
      .subscribe((site: HealthAuthoritySiteAdmin) => {
        this.site = site;
        this.initForm(site);
      });

    this.pec.markAsTouched();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ]
    });
  }

  private initForm({ pec }: HealthAuthoritySiteAdmin): void {
    this.form.patchValue({ pec });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.route.snapshot.params.sid, value);
  }
}
