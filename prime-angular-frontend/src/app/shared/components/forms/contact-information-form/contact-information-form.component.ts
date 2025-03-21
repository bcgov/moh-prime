import { Component, ContentChildren, Input, OnInit, QueryList } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';

import { NotificationInfoSummaryDirective } from './notification-info-summary.directive';

@Component({
  selector: 'app-contact-information-form',
  templateUrl: './contact-information-form.component.html',
  styleUrls: ['./contact-information-form.component.scss']
})
export class ContactInformationFormComponent implements OnInit {
  /**
   * @description
   * Contact information form instance.
   */
  @Input() public form: UntypedFormGroup;
  /**
   * @description
   * Mode for displaying the form fields where full includes
   * all fields, and partial only includes phone and email.
   */
  @Input() public mode: 'full' | 'partial';
  /**
   * @description
   * Whether to show the SMS phone field.
   *
   * NOTE: Only applies in "full" mode.
   */
  @Input() public showSmsPhone: boolean;
  @Input() public contactDescription: string;

  @ContentChildren(NotificationInfoSummaryDirective, { descendants: true })
  public notificationInfoSummaryChildren: QueryList<NotificationInfoSummaryDirective>;

  constructor() {

    this.showSmsPhone = true;
    this.contactDescription = 'Provide a phone number that may be used to contact you.';
  }

  public get phone(): UntypedFormControl {
    return this.form.get('phone') as UntypedFormControl;
  }

  public get phoneExtension(): UntypedFormControl {
    return this.form.get('phoneExtension') as UntypedFormControl;
  }

  public get email(): UntypedFormControl {
    return this.form.get('email') as UntypedFormControl;
  }

  public get smsPhone(): UntypedFormControl {
    return this.form.get('smsPhone') as UntypedFormControl;
  }

  public get hasCustomNotificationInfoSummary(): boolean {
    return !!this.notificationInfoSummaryChildren.length;
  }

  public ngOnInit(): void { }
}
