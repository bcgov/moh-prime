import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { pairwise, distinctUntilChanged, startWith } from 'rxjs/operators';

import { Config, ProvinceConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

import { AddressLine } from '@lib/types/address-line.type';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  // Country must be included in the FormGroup, but
  // does not have to be displayed
  @Input() public form: FormGroup;
  // List of controls that should be displayed
  @Input() public formControlNames: AddressLine[];
  // Whether BC addresses can only be selected using autocomplete
  @Input() bcOnly: boolean;
  // Wether to show the add manual address button, default to false
  @Input() showManualButton: boolean;
  // Wether to show the address fields, default to true
  @Input() showAddressFields: boolean;

  public countries: Config<string>[];
  // Includes provinces and states
  public provinces: ProvinceConfig[];
  // Filtered based on country
  public filteredProvinces: ProvinceConfig[];
  public provinceLabel: string;
  public postalLabel: string;
  public postalMask: string;

  constructor(
    private configService: ConfigService,
    private formUtilsService: FormUtilsService
  ) {
    this.formControlNames = [
      'countryCode',
      'provinceCode',
      'street',
      'street2',
      'city',
      'postal'
    ];
    this.countries = this.configService.countries;
    this.provinces = this.configService.provinces;
    this.setAddressLabels();
    this.showManualButton = false;
    this.showAddressFields = true;
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

  public showFormControl(formControlName: AddressLine): boolean {
    return this.formControlNames.includes(formControlName);
  }

  public getFormControlOrder(formControlName: AddressLine): string {
    const index = this.formControlNames.indexOf(formControlName) + 1;
    return `order-${index}`;
  }

  public isRequired(addressLine: AddressLine): boolean {
    return this.formUtilsService.isRequired(this.form, addressLine);
  }

  public onAutocomplete({ id, countryCode, ...address }: Address) {
    // Populate the associated list of associated provinces/states
    this.countryCode.patchValue(countryCode);
    // Patch the remaining address, which includes the province/state
    this.form.patchValue(address);
    // show the manual address fields upon selection
    this.showManualAddress();
  }

  public showManualAddress() {
    this.showAddressFields = true;
  }

  public ngOnInit() {
    this.initForm();
  }

  private initForm() {
    this.setAddress(this.countryCode.value);
    this.countryCode.valueChanges
      .pipe(
        startWith(Country.CANADA),
        pairwise(),
        distinctUntilChanged()
      )
      .subscribe(([prevCountry, nextCountry]: [string, string]) => {
        if (prevCountry !== nextCountry) {
          this.provinceCode.reset();
          this.postal.reset();
        }
        this.setAddress(nextCountry);
      });
    if (this.showManualButton) {
      // For initialization, show the form if it's valid otherwise hide
      this.showAddressFields = this.form.valid;
    }
    // Always show address fields when show manual button is not present
    else {
      this.showAddressFields = true;
    }
  }

  private setAddress(countryCode: string) {
    this.filteredProvinces = this.provinces.filter(p => p.countryCode === this.countryCode.value);

    this.setAddressLabels(countryCode);
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
