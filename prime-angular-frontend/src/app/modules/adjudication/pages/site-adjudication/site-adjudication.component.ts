import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, Observable, BehaviorSubject } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-adjudication',
  templateUrl: './site-adjudication.component.html',
  styleUrls: ['./site-adjudication.component.scss']
})
export class SiteAdjudicationComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public hasActions: boolean;
  public refresh: BehaviorSubject<boolean>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private siteResource: SiteResource
  ) {
    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = this.route.snapshot.params.sid;
      this.busy = this.siteResource
        .updatePecCode(siteId, this.form.value.pec)
        .subscribe(() => this.refresh.next(true));
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.busy = this.getSite()
      .subscribe((site: Site) => this.form.patchValue(site));
  }

  private createFormInstance() {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required]
      ]
    });
  }

  private getSite(): Observable<Site> {
    const siteId = this.route.snapshot.params.sid;
    return this.siteResource.getSiteById(siteId);
  }
}
