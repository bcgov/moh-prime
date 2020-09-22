import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

import { EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { Address } from '@shared/models/address.model';
import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@shared/models/address-autocomplete.model';
import { AddressAutocompleteResource } from '@shared/services/address-autocomplete-resource.service';

@Component({
  selector: 'app-address-autocomplete',
  templateUrl: './address-autocomplete.component.html',
  styleUrls: ['./address-autocomplete.component.scss']
})
export class AddressAutocompleteComponent implements OnInit {
  @Input() bcOnly: boolean;
  @Output() autocompleteAddress: EventEmitter<Address>;

  public form: FormGroup;
  public addressRetrieved: AddressAutocompleteRetrieveResponse;
  public addressAutocompleteFields: AddressAutocompleteFindResponse[];

  constructor(
    private fb: FormBuilder,
    private addressAutocompleteResource: AddressAutocompleteResource,
    private toastService: ToastService
  ) {
    this.autocompleteAddress = new EventEmitter<Address>();
    this.bcOnly = false;
  }

  public get autocomplete(): FormControl {
    return this.form.get('autocomplete') as FormControl;
  }

  public onAutocomplete(id: string) {
    this.addressAutocompleteResource.retrieve(id)
      .subscribe((results: AddressAutocompleteRetrieveResponse[]) => {
        this.addressRetrieved = results.find(result => result.language === 'ENG') ?? null;

        if (this.addressRetrieved) {
          const address = new Address(
            this.addressRetrieved.countryIso2,
            this.addressRetrieved.provinceCode,
            this.addressRetrieved.line1,
            this.addressRetrieved.line2,
            this.addressRetrieved.city,
            this.addressRetrieved.postalCode
          );

          (!this.bcOnly || address.provinceCode === 'BC')
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
        exhaustMap((value: string) => {
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
