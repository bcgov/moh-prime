import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';

import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { PhsaEformsFormStateService } from '@phsa/shared/services/phsa-eforms-form-state.service';

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
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected phsaEformsFormStateService: PhsaEformsFormStateService,
  ) { }

  public get accessCode(): FormControl {
    return this.form.get('accessCode') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.router.navigate([PhsaEformsRoutes.DEMOGRAPHIC], {
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
    this.form = this.phsaEformsFormStateService.accessCodeFormState.form;
  }
}
