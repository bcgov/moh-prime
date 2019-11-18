import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { Config, ProvinceConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { Country } from '@shared/enums/country.enum';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  @Input() public form: FormGroup;

  public countries: Config<string>[];
  public provinces: ProvinceConfig[];
  public filteredProvinces: ProvinceConfig[];
  public provinceLabel: string;
  public postalLabel: string;
  public postalMask: string;

  constructor(
    private configService: ConfigService,
    private formUtilsService: FormUtilsService
  ) {
    this.countries = this.configService.countries;
    this.provinces = this.configService.provinces;
    this.setAddressLabels();
  }

  public get countryCode(): FormControl {
    return this.form.get('countryCode') as FormControl;
  }

  public get provinceCode(): FormControl {
    return this.form.get('provinceCode') as FormControl;
  }

  public get postal(): FormControl {
    return this.form.get('postal') as FormControl;
  }

  public isRequired(path: string): boolean {
    return this.formUtilsService.isRequired(this.form, path);
  }

  public ngOnInit() {
    this.initForm();
  }

  private initForm() {
    this.countryCode.valueChanges
      .subscribe((countryCode: string) => {
        this.provinceCode.reset();
        this.postal.reset();
        this.filteredProvinces = this.provinces.filter(p => p.countryCode === countryCode);
        this.setAddressLabels(countryCode);
      });
  }

  private setAddressLabels(countryCode: string = Country.CANADA): void {
    const { province, postal } = this.addressConfig(countryCode);
    this.provinceLabel = province;
    this.postalLabel = postal.label;
    this.postalMask = postal.mask;
  }

  private addressConfig(countryCode: string) {
    switch (countryCode) {
      case Country.UNITED_STATES:
        return { province: 'State', postal: { label: 'Zip Code', mask: '00000' } };
      case Country.CANADA:
      default:
        return { province: 'Province', postal: { label: 'Postal Code', mask: 'S0S 0S0' } };
    }
  }
}
