import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';

import { PhsaFormStateService } from '@phsa/shared/services/phsa-labtech-form-state.service';
import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';

@Component({
  selector: 'app-access-code',
  templateUrl: './access-code.component.html',
  styleUrls: ['./access-code.component.scss']
})
export class AccessCodeComponent implements OnInit {

  public form: FormGroup;

  public constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected phsaLabtechFormStateService: PhsaFormStateService,
  ) { }

  public get accessCode(): FormControl {
    return this.form.get('accessCode') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.router.navigate([ PhsaLabtechRoutes.DEMOGRAPHIC ], {
        relativeTo: this.route.parent
      });
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  protected createFormInstance() {
    this.form = this.phsaLabtechFormStateService.accessForm;
  }
}
