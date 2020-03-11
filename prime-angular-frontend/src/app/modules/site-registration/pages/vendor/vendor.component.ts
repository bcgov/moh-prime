import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { BehaviorSubject } from 'rxjs';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss']
})
export class VendorComponent implements OnInit {
  form: FormGroup;
  vendorsData = [
    { id: 0, name: 'Excelleris' },
    { id: 1, name: 'iClinic Inc.' },
    { id: 2, name: 'Medinet' },
    { id: 3, name: 'Plexia Electronic Medical Systems' },
    { id: 4, name: 'CareConnect (geographically limited access)' }
  ];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService,
    private formBuilder: FormBuilder
  ) {
  }

  onChangeEventFunc(name: string, isChecked: boolean) {
    const vendors = (this.form.controls.vendors as FormArray);

    if (isChecked) {
      vendors.push(new FormControl(name));
    } else {
      const index = vendors.controls.findIndex(x => x.value === name);
      vendors.removeAt(index);
    }
  }

  onSubmit() {
    console.log(this.form.value.vendors);
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.SIGNING_AUTHORITY], { relativeTo: this.route.parent });
  }

  onBack() {
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      vendors: new FormArray([])
    });

  }



}
