import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@shared/models/address-autocomplete.model';
import { AddressValidationResource } from '@shared/services/address-validation-resource.service';

@Component({
  selector: 'app-address-autocomplete',
  templateUrl: './address-autocomplete.component.html',
  styleUrls: ['./address-autocomplete.component.scss']
})
export class AddressAutocompleteComponent implements OnInit {
  @Input() public form: FormGroup;
  @Output()

  public addressAutocompleteFields: AddressAutocompleteFindResponse[];
  public addressRetrieved: AddressAutocompleteRetrieveResponse;

  constructor(
    private addressValidationResource: AddressValidationResource
  ) { }

  public get autocomplete(): FormControl {
    return this.form.get('autocomplete') as FormControl;
  }

  public onAutocomplete(id: string){
    this.addressValidationResource.retrieve(id)
      .subscribe((response: AddressAutocompleteRetrieveResponse) =>
          this.addressRetrieved = response
      );
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
