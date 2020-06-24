import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@registration/shared/services/site-resource.service';
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private siteResource: SiteResource
  ) {
    this.hasActions = true;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = this.route.snapshot.params.sid;
      console.log(this.form.value.pec, this.form.value);

      this.siteResource.updatePecCode(siteId, this.form.value.pec)
        .pipe()
        .subscribe();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    const siteId = this.route.snapshot.params.sid;
    this.busy = this.siteResource.getSiteById(siteId)
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
}
