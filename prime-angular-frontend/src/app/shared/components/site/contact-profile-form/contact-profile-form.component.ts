import {
  Component,
  OnInit,
  Input,
  ContentChildren,
  QueryList,
  ContentChild,
  AfterContentInit,
  ViewChild
} from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { FormUtilsService } from '@core/services/form-utils.service';
import { PageSubheader2MoreInfoDirective } from '@shared/components/pages/page-subheader2/page-subheader2-more-info.directive';
import { SameAsComponent } from '@shared/components/site/same-as/same-as.component';
import { Address } from '@lib/models/address.model';
import { Contact } from '@lib/models/contact.model';
import { PageFooterComponent } from '@shared/components/pages/page-footer/page-footer.component';

@Component({
  selector: 'app-contact-profile-form',
  templateUrl: './contact-profile-form.component.html',
  styleUrls: ['./contact-profile-form.component.scss']
})
export class ContactProfileFormComponent implements OnInit, AfterContentInit {
  /**
   * @description
   * Title of the contact profile section.
   */
  @Input() public title: string;
  /**
   * @description
   * Instance of the contact profile form.
   */
  @Input() public form: FormGroup;
  /**
   * @description
   * List of fields that should be excluded.
   */
  @Input() public excludeList: ('fax' | 'jobRoleTitle' | 'physicalAddress')[];
  /**
   * @description
   * Whether access to the address should be gated
   * by a toggle (default: true).
   *
   * NOTE: Makes the address required when no toggle exists.
   */
  @Input() public hasAddressToggle: boolean;
  /**
   * @description
   * Pass control of the address line fields being
   * displayed to the parent component.
   */
  @Input() public showAddressFields: boolean;
  /**
   * @description
   * Reference to the address toggle.
   */
  @ViewChild(MatSlideToggle, { static: true }) public toggle: MatSlideToggle;
  /**
   * @description
   * Optional projection of the SameAsComponent for
   * auto-filling in the contact profile form.
   */
  @ContentChild(SameAsComponent) public sameAs: SameAsComponent;
  /**
   * @description
   * Contact specific profile information.
   */
  @ContentChildren(PageSubheader2MoreInfoDirective)
  public pageSubheaderMoreInfoChildren: QueryList<PageSubheader2MoreInfoDirective>;
  /**
   * @description
   * Reference to page footer.
   *
   * NOTE: Projected to allow for listening to the
   * triggered save (aka submission) event.
   */
  @ContentChild(PageFooterComponent) public pageFooter: PageFooterComponent;

  constructor(
    private formUtilsService: FormUtilsService
  ) {
    this.excludeList = [];
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

  /**
   * @description
   * Event handler for the address toggle.
   */
  public onAddressChange({ checked }: MatSlideToggleChange) {
    if (!this.hasAddressToggle || this.excludeList.includes('physicalAddress')) {
      return;
    }

    if (checked) {
      this.physicalAddress.reset();
    }

    this.changeAddressValidators(this.physicalAddress);
  }

  public ngOnInit(): void {
    this.changeAddressValidators(this.physicalAddress);

    if (this.toggle) {
      // Check for profile information outside of the address to
      // determine if this is an update, and the address is empty
      // in order to set the toggle appropriately
      const { physicalAddress, ...profile } = this.form.value;
      const isUpdate = Object.keys(profile).some(k => profile[k]);

      if (isUpdate && Address.isEmpty(physicalAddress)) {
        // Assumed that on update and no address that address
        // is the same as the site address
        this.toggleAddress(true);
      }
    }
  }

  public ngAfterContentInit(): void {
    if (this.sameAs) {
      // Ensure the address line fields are displayed after
      // being patched by same-as
      this.sameAs.selected
        .subscribe((contact: Contact) => {
          if (Address.isNotEmpty(contact.physicalAddress)) {
            // Change the validators based on the toggled value, otherwise
            // the default is required so the address should be displayed
            this.toggleAddress();
            this.expandAddressFields();
          }
        });
    }

    if (this.pageFooter) {
      this.pageFooter.save
        .subscribe(() => {
          // Change the validators based on the toggled value, otherwise
          // the default is required so the address should be displayed
          if (!this.toggle?.checked) {
            this.changeAddressValidators(this.physicalAddress);
          }
          this.expandAddressFields();
        });
    }
  }

  /**
   * @description
   * Expand the visibility of the address fields
   * beyond autocomplete.
   */
  private expandAddressFields() {
    if (!this.excludeList.includes('physicalAddress')) {
      this.showAddressFields = true;
    }
  }

  /**
   * @description
   * Change the toggled visibility of the address fields based
   * on the toggled value, otherwise NOOP.
   */
  private toggleAddress(toggleAddress: boolean = false) {
    if (this.toggle && !this.excludeList.includes('physicalAddress')) {
      this.toggle.checked = toggleAddress;
      this.changeAddressValidators(this.physicalAddress);
    }
  }

  /**
   * @description
   * Apply or reset address validators based on the toggle existence.
   */
  private changeAddressValidators(address: FormGroup, blacklist: string[] = ['id', 'street2']) {
    if (!this.excludeList.includes('physicalAddress')) {
      if (this.toggle) {
        (this.toggle.checked)
          ? this.formUtilsService.resetAndClearValidators(address)
          : this.formUtilsService.setValidators(address, [Validators.required], blacklist);
      } else if (!this.hasAddressToggle) {
        this.formUtilsService.setValidators(address, [Validators.required], blacklist);
      }
    }
  }
}

