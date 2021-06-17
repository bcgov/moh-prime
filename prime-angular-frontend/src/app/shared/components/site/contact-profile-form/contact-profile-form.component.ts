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
import { Address } from '@shared/models/address.model';

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
   * Show the optional fax field.
   */
  @Input() public showFax: boolean;
  /**
   * @description
   * Pass control of the address line fields being
   * displayed to the parent component.
   */
  @Input() public showAddressFields: boolean;
  @ViewChild(MatSlideToggle) public toggle: MatSlideToggle;
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
  @ContentChildren(PageSubheader2MoreInfoDirective, { descendants: true })
  public pageSubheaderMoreInfoChildren: QueryList<PageSubheader2MoreInfoDirective>;
  /**
   * @description
   * Whether the toggle is checked, which displays
   * the address component.
   */
  public toggleAddress: boolean;

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

    this.toggleAddress = !this.toggleAddress;
    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  public togglePhysicalAddress() {
    const isNotEmpty = Address.isNotEmpty(this.physicalAddress.value);
    if (isNotEmpty) {
      // Always show when the address is not empty
      // regardless of the input binding value
      this.toggleAddress = isNotEmpty;
    }
    this.togglePhysicalAddressValidators(this.physicalAddress, ['id', 'street2']);
  }

  public ngOnInit(): void {
    this.togglePhysicalAddress();
  }

  public ngAfterContentInit(): void {
    if (this.sameAs) {
      // Ensure the address line fields are displayed after
      // being patched by same as
      this.sameAs.selected
        .subscribe(() => {
          this.togglePhysicalAddress();
          this.showAddressFields = true;
        });
    }
  }

  private togglePhysicalAddressValidators(separateAddress: FormGroup, blacklist: string[] = []) {
    (!this.toggleAddress)
      ? this.formUtilsService.resetAndClearValidators(separateAddress)
      : this.formUtilsService.setValidators(separateAddress, [Validators.required], blacklist);
  }
}

