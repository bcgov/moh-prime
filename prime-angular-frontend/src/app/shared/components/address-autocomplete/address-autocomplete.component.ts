import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

import { EMPTY } from 'rxjs';
import { debounceTime, exhaustMap, switchMap } from 'rxjs/operators';

import { Address } from '@lib/models/address.model';
import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@lib/models/address-autocomplete.model';
import { ToastService } from '@core/services/toast.service';
import { AddressAutocompleteResource } from '@shared/services/address-autocomplete-resource.service';

@Component({
  selector: 'app-address-form-autocomplete',
  templateUrl: './address-autocomplete.component.html',
  styleUrls: ['./address-autocomplete.component.scss']
})
export class AddressAutocompleteComponent implements OnInit {
  @Input() inBc: boolean;
  @Output() autocompleteAddress: EventEmitter<Address>;

  public form: FormGroup;
  public addressAutocompleteFields: AddressAutocompleteFindResponse[];

  constructor(
    private fb: FormBuilder,
    private addressAutocompleteResource: AddressAutocompleteResource,
    private toastService: ToastService
  ) {
    this.autocompleteAddress = new EventEmitter<Address>();
    this.inBc = false;
  }

  public get autocomplete(): FormControl {
    return this.form.get('autocomplete') as FormControl;
  }

  public onAutocomplete(id: string) {
    this.addressAutocompleteResource.retrieve(id)
      .subscribe((results: AddressAutocompleteRetrieveResponse[]) => {
        const addressRetrieved = results.find(result => result.language === 'ENG') ?? null;

        if (addressRetrieved) {
          const address = new Address(
            addressRetrieved.countryIso2,
            addressRetrieved.provinceCode,
            addressRetrieved.line1,
            addressRetrieved.line2,
            addressRetrieved.city,
            addressRetrieved.postalCode
          );

          (!this.inBc || address.provinceCode === 'BC')
            ? this.autocompleteAddress.emit(address)
            : this.toastService.openErrorToast('Address must be located in BC');

        } else {
          this.toastService.openErrorToast('Address could not be retrieved');
        }
      });
  }

  public ngOnInit(): void {
    this.form = this.fb.group({ autocomplete: [''] });

    this.autocomplete.valueChanges
      .pipe(
        debounceTime(400),
        switchMap((value: string) => {
          this.addressAutocompleteFields = [];
          return (value)
            ? this.addressAutocompleteResource.find(value)
            : EMPTY;
        })
      )
      .subscribe((response: AddressAutocompleteFindResponse[]) =>
        this.addressAutocompleteFields = response
      );
  }
}
