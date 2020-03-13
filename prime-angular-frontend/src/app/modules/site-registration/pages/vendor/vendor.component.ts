import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '@core/services/toast.service';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss']
})
export class VendorComponent implements OnInit {
  public form: FormGroup;
  public vendorsData = [];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) {
    this.vendorsData = [
      { id: 0, name: 'Excelleris' },
      { id: 1, name: 'iClinic Inc.' },
      { id: 2, name: 'Medinet' },
      { id: 3, name: 'Plexia Electronic Medical Systems' },
      { id: 4, name: 'CareConnect (geographically limited access)' }
    ];
  }

  public onChangeEventFunc(name: string, isChecked: boolean) {
    const vendors = (this.form.controls.vendors as FormArray);

    if (isChecked) {
      vendors.push(new FormControl(name));
    } else {
      const index = vendors.controls.findIndex(x => x.value === name);
      vendors.removeAt(index);
    }
  }

  public onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.SIGNING_AUTHORITY], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.form = this.formBuilder.group({
      vendors: new FormArray([])
    });
  }
}
