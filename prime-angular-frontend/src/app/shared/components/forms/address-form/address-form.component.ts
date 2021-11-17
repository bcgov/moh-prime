import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { pairwise, distinctUntilChanged, startWith } from 'rxjs/operators';

import { Config, ProvinceConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

import { Country } from '@lib/enums/country.enum';
import { Address, AddressLine } from '@lib/models/address.model';
import { FormUtilsService } from '@core/services/form-utils.service';

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.scss']
})
export class AddressFormComponent implements OnInit {
  /**
   * @description
   * Address line form.
   */
  @Input() public form: FormGroup;
  /**
   * @description
   * List of address line controls that should be
   * displayed.
   *
   * NOTE: Country must be included, but does not
   * have to be displayed
   */
  @Input() public formControlNames: AddressLine[];
  /**
   * @description
   * Whether BC addresses can only be selected using
   * autocomplete.
   */
  @Input() public inBc: boolean;
  /**
   * @description
   * Whether to show the manual address.
   */
  @Input() public showManualButton: boolean;
  /**
   * @description
   * Whether to show the address line fields.
   */
  @Input() public showAddressFields: boolean;

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

  public onAutocomplete({ id, countryCode, ...address }: Address): void {
    // Populate the associated list of associated provinces/states
    this.countryCode.patchValue(countryCode);
    // Patch the remaining address fields, which includes the province/state
    this.form.patchValue(address);
    this.showManualAddress();
  }

  public showManualAddress(): void {
    this.showAddressFields = true;
  }

  public ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
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

    if (Address.isNotEmpty(this.form.value)) {
      // Only every set to true when the address is not empty, otherwise
      // leave control in the hands of the component or input bindings
      this.showAddressFields = true;
    }
  }

  private setAddress(countryCode: string): void {
    this.filteredProvinces = this.provinces.filter(p => p.countryCode === this.countryCode.value);
    this.setAddressLabels(countryCode);
  }

  private setAddressLabels(countryCode: string = Country.CANADA): void {
    const { province, postal } = this.addressConfig(countryCode);
    this.provinceLabel = province;
    this.postalLabel = postal.label;
    this.postalMask = postal.mask;
  }

  private addressConfig(countryCode: string): { province: string, postal: { label: string, mask: string } } {
    switch (countryCode) {
      case Country.UNITED_STATES:
        return { province: 'State', postal: { label: 'Zip Code', mask: '00000' } };
      case Country.CANADA:
      default:
        return { province: 'Province', postal: { label: 'Postal Code', mask: 'S0S 0S0' } };
    }
  }
}
