import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { BehaviorSubject, Observable, combineLatest, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { VendorConfig } from '@config/config.model';

@UntilDestroy()
@Component({
  selector: 'app-options-form',
  templateUrl: './options-form.component.html',
  styleUrls: ['./options-form.component.scss']
})
export class OptionsFormComponent implements OnInit {
  /**
   * @description
   * Instance of form.
   */
  @Input() public form: FormGroup;
  /**
   * @description
   * Index of the current FormGroup within the FormArray.
   */
  @Input() public index: number;
  /**
   * @description
   * Set of available select options.
   */
  @Input() public availableOptions: BehaviorSubject<any[]>;
  /**
   * @description
   * Form input label.
   */
  @Input() public selectLabel: string;
  /**
   * @description
   * Key for accessing the option
   */
  @Input() public optionLabel: string;
  /**
   * @description
   * FormGroup control name.
   */
  @Input() public controlName: string;
  /**
   * @description
   * Allow removal of the FormGroup from the FormArray.
   */
  @Input() public allowRemoval: boolean;
  /**
   * @description
   * Remove control event emitter.
   */
  @Output() public remove: EventEmitter<number>;

  public filteredOptions: Observable<any[]>;

  constructor() {
    this.remove = new EventEmitter<number>();
  }

  public removeControl(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit() {
    this.filteredOptions = this.initialize();
  }

  private initialize() {
    return combineLatest([
      this.availableOptions.asObservable(), // Prevent accidentally affecting parent observable
      this.form.valueChanges.pipe(startWith('')) // Trigger emission immediately!
    ]).pipe(
      untilDestroyed(this),
      map(([availableOptions, currentOption]: [any[], any]) =>
        this.filterOptions(availableOptions, currentOption)
      )
    );
  }

  /**
   * @description
   * Filtering of the available options.
   */
  private filterOptions(availableVendors: VendorConfig[], { vendorCode }: { vendorCode: number }): VendorConfig[] {
    // Default provide the entire list
    let filteredVendors = availableVendors;

    if (availableVendors.length) {
      filteredVendors = filteredVendors.filter(
        // TODO make this a passed predicate for reuse
        (vendor: VendorConfig) => vendor.code === vendorCode
      );
    }

    return filteredVendors;
  }
}
