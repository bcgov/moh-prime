import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '@core/services/toast.service';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-site-information',
  templateUrl: './site-information.component.html',
  styleUrls: ['./site-information.component.scss']
})
export class SiteInformationComponent implements OnInit {
  public form: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) { }

  public get siteName(): FormControl {
    return this.form.get('siteName') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public get street(): FormControl {
    return this.form.get('street') as FormControl;
  }

  public get city(): FormControl {
    return this.form.get('city') as FormControl;
  }

  public get postal(): FormControl {
    return this.form.get('postal') as FormControl;
  }

  public onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = this.formBuilder.group({
      siteName: [null, []],
      doingBusinessAs: [null, []],
      street: [{ value: null, disabled: false }, []],
      city: [{ value: null, disabled: false }, []],
      postal: [{ value: null, disabled: false }, []]
    });
  }

}
