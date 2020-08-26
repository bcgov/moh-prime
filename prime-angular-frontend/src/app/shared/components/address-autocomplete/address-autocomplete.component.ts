import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@shared/models/address-autocomplete.model';
import { AddressValidationResource } from '@shared/services/address-validation-resource.service';
import { Address } from '@shared/models/address.model';

@Component({
  selector: 'app-address-autocomplete',
  templateUrl: './address-autocomplete.component.html',
  styleUrls: ['./address-autocomplete.component.scss']
})
export class AddressAutocompleteComponent implements OnInit {
  @Output() autocompleteAddress: EventEmitter<Address>;

  public addressAutocompleteFields: AddressAutocompleteFindResponse[];
  public addressRetrieved: AddressAutocompleteRetrieveResponse;
  public form: FormGroup;

  constructor(
    private addressValidationResource: AddressValidationResource
  ) {
    this.autocompleteAddress = new EventEmitter<Address>();
  }

  public get autocomplete(): FormControl {
    return this.form.get('autocomplete') as FormControl;
  }

  public onAutocomplete(id: string) {
    this.addressValidationResource.retrieve(id)
      .subscribe((response: AddressAutocompleteRetrieveResponse[]) => {
        response.map((field) => {
          if (field.language === 'ENG') {
            this.addressRetrieved = field;
          }
        });
        const address = new Address();
        address.countryCode = this.addressRetrieved.countryIso2;
        address.provinceCode = this.addressRetrieved.provinceCode;
        address.city = this.addressRetrieved.city;
        address.street = this.addressRetrieved.line1;
        address.street2 = this.addressRetrieved.line2;
        address.postal = this.addressRetrieved.postalCode;
        this.autocompleteAddress.emit(address);
      });
  }

  ngOnInit(): void {
    this.form = new FormGroup({ autocomplete: new FormControl() });

    this.autocomplete.valueChanges
      .subscribe(() => {
        this.addressValidationResource.find(this.autocomplete.value)
          .subscribe((response: AddressAutocompleteFindResponse[]) => {
            this.addressAutocompleteFields = response;
          });
      });
  }

}
