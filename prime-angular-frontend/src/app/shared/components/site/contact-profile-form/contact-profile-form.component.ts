import { Component, OnInit, Input, ContentChildren, QueryList, ViewChild } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable, Subject } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { FormUtilsService } from '@core/services/form-utils.service';
import { PageSubheader2MoreInfoDirective } from '@shared/components/pages/page-subheader2/page-subheader2-more-info.directive';
import { Contact } from '@lib/models/contact.model';

@UntilDestroy()
@Component({
  selector: 'app-contact-profile-form',
  templateUrl: './contact-profile-form.component.html',
  styleUrls: ['./contact-profile-form.component.scss']
})
export class ContactProfileFormComponent implements OnInit {
  @Input() public title: string;
  @Input() public form: FormGroup;
  @Input() public showFax: boolean;
  @Input() public showAddButton: boolean;
  @Input() public contacts: Contact[];
  @Input() public formSubmitting: Observable<void> = new Observable<void>();
  public showFormFields: boolean;
  public hasPhysicalAddress: boolean;
  public showAddressFields: boolean;
  @ContentChildren(PageSubheader2MoreInfoDirective, { descendants: true })
  public pageSubheaderMoreInfoChildren: QueryList<PageSubheader2MoreInfoDirective>;
  @ViewChild('sameAddressSlideToggle')
  public sameAddressSlideToggle: MatSlideToggle;

  constructor(
    private formUtilsService: FormUtilsService
  ) {
    this.showFax = true;
    this.showAddButton = false;
  }

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
    if (checked) {
      this.physicalAddress.reset();
    }

    this.hasPhysicalAddress = !this.hasPhysicalAddress;
    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  public updateContact(contact: Contact): void {
    this.form.patchValue(contact);
    this.showFormFields = true;
  }

  public addContact(): void {
    this.showFormFields = true;
  }

  public ngOnInit() {
    // When the street is populated ensure the address is shown
    this.physicalAddress.get('street')
      .valueChanges
      .pipe(distinctUntilChanged())
      .subscribe((value: string) => (value) ? this.togglePhysicalAddress() : null);

    // If first time loading, force address slider to be 'off' by default
    if (!this.form.get('firstName').value) {
      this.togglePhysicalAddress(true);
    }
    else {
      this.togglePhysicalAddress();
    }

    this.formSubmitting.pipe(untilDestroyed(this))
      .subscribe(() => {
        // force show address fields if address form is invalid
        this.showAddressFields = !this.sameAddressSlideToggle.checked && this.physicalAddress.invalid;
      });

    this.showFormFields = !this.contacts?.length || !this.hasPhysicalAddress;
  }

  private togglePhysicalAddress(forceDefault?: boolean) {
    this.hasPhysicalAddress = !!(
      this.physicalAddress.get('countryCode').value ||
      this.physicalAddress.get('provinceCode').value ||
      this.physicalAddress.get('street').value ||
      this.physicalAddress.get('street2').value ||
      this.physicalAddress.get('city').value ||
      this.physicalAddress.get('postal').value
    ) || forceDefault;

    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  private togglePhysicalAddressValidators(separateAddress: FormGroup, blacklist: string[] = []) {
    (!this.hasPhysicalAddress)
      ? this.formUtilsService.resetAndClearValidators(separateAddress)
      : this.formUtilsService.setValidators(separateAddress, [Validators.required], blacklist);
  }
}
