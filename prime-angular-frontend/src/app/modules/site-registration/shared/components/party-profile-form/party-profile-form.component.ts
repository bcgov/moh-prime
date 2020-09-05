import { Component, OnInit, Input, ContentChildren } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { distinctUntilChanged } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { PageSubheader2MoreInfoDirective } from '@shared/components/page-subheader2/page-subheader2-more-info.directive';

@Component({
  selector: 'app-party-profile-form',
  templateUrl: './party-profile-form.component.html',
  styleUrls: ['./party-profile-form.component.scss']
})
export class PartyProfileFormComponent implements OnInit, AfterContentInit {
  @Input() public title: string;
  @Input() public form: FormGroup;
  @ContentChildren(PageSubheader2MoreInfoDirective, { descendants: true })
  public pageSubheaderMoreInfoChildren: QueryList<PageSubheader2MoreInfoDirective>;
  public hasPhysicalAddress: boolean;

  constructor(
    private formUtilsService: FormUtilsService
  ) { }

  public get hasPageSubheaderMoreInfo(): boolean {
    return !!this.pageSubheaderMoreInfoChildren.length;
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get fax(): FormControl {
    return this.form.get('fax') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.form.get('smsPhone') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public onPhysicalAddressChange({ checked }: MatSlideToggleChange) {
    if (!checked) {
      this.physicalAddress.reset();
    }

    this.hasPhysicalAddress = !this.hasPhysicalAddress;
    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  public ngOnInit() {
    // When the street is populated ensure the address is shown
    this.physicalAddress.get('street')
      .valueChanges
      .pipe(distinctUntilChanged())
      .subscribe((value: string) => (value) ? this.togglePhysicalAddress() : null);

    this.togglePhysicalAddress();
  }

  private togglePhysicalAddress() {
    this.hasPhysicalAddress = !!(
      this.physicalAddress.get('countryCode').value ||
      this.physicalAddress.get('provinceCode').value ||
      this.physicalAddress.get('street').value ||
      this.physicalAddress.get('street2').value ||
      this.physicalAddress.get('city').value ||
      this.physicalAddress.get('postal').value
    );

    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  private togglePhysicalAddressValidators(separateAddress: FormGroup, blacklist: string[] = []) {
    (!this.hasPhysicalAddress)
      ? this.formUtilsService.resetAndClearValidators(separateAddress)
      : this.formUtilsService.setValidators(separateAddress, [Validators.required], blacklist);
  }
}
