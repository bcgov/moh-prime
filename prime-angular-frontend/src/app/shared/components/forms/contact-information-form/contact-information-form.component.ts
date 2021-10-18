import { Component, ContentChildren, Input, OnInit, QueryList } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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
  @Input() public form: FormGroup;
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

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get phoneExtension(): FormControl {
    return this.form.get('phoneExtension') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.form.get('smsPhone') as FormControl;
  }

  public get hasCustomNotificationInfoSummary(): boolean {
    return !!this.notificationInfoSummaryChildren.length;
  }

  public ngOnInit(): void { }
}
