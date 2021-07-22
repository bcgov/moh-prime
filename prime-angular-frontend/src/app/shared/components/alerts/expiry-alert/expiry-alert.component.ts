import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

import { Moment } from 'moment';

import { DateUtils } from '@lib/utils/date-utils.class';
import { AlertType } from '@shared/components/alerts/alert/alert.component';

@Component({
  selector: 'app-expiry-alert',
  templateUrl: './expiry-alert.component.html',
  styleUrls: ['./expiry-alert.component.scss']
})
export class ExpiryAlertComponent implements OnInit, OnChanges {
  /**
   * @description
   * Alert type indicating the theme.
   */
  @Input() public type: AlertType;
  /**
   * @description
   * Alert specific icon.
   */
  @Input() public icon: string;
  /**
   * @description
   * Date(s) that expiry will occur.
   */
  @Input() public expiryDates: (string | Moment) | (string | Moment)[];
  /**
   * @description
   * Show the alert when the expiry date is within
   * the specified number of days.
   */
  @Input() public daysToExpiry: number;

  public withinDaysOfRenewal: boolean;

  constructor() {
    this.type = 'info';
    this.icon = 'error_outline';
    this.daysToExpiry = 90;
  }

  public ngOnChanges(changes: SimpleChanges): void {
    const currentExpiryDates = changes.expiryDates.currentValue;
    if (currentExpiryDates) {
      this.updateWithinDaysOfRenewal(currentExpiryDates);
    }
  }

  public ngOnInit(): void { }

  private updateWithinDaysOfRenewal(updatedExpiryDates: (string | Moment)[]): void {
    const expiryDates = (Array.isArray(updatedExpiryDates))
      ? updatedExpiryDates
      : [updatedExpiryDates];
    this.withinDaysOfRenewal = expiryDates
      .map((expiryDate: (string | Moment)) => DateUtils.withinDaysBeforeDate(expiryDate, this.daysToExpiry))
      .some((withinDaysOfRenewal: boolean) => withinDaysOfRenewal);
  }
}
