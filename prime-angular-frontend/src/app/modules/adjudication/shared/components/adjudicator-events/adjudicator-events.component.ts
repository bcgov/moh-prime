import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { EnumUtils } from '@lib/utils/enum-utils.class';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { CasePipe } from '@shared/pipes/case.pipe';

@Component({
  selector: 'app-adjudicator-events',
  templateUrl: './adjudicator-events.component.html',
  styleUrls: ['./adjudicator-events.component.scss']
})
export class AdjudicatorEventsComponent implements OnInit {
  @Input() public businessEvents$: Observable<BusinessEvent[]>;
  @Output() public getBusinessEvents: EventEmitter<BusinessEventTypeEnum[]>;

  public form: UntypedFormGroup;
  public businessEventTypes: { key: number, value: string }[];

  constructor(
    private fb: UntypedFormBuilder,
    private casePipe: CasePipe,
    private capitalizePipe: CapitalizePipe
  ) {
    this.getBusinessEvents = new EventEmitter<BusinessEventTypeEnum[]>();
    this.businessEventTypes = EnumUtils.asObjects(BusinessEventTypeEnum)
      .map((businessEventTypeObj: { key: number, value: string }) => {
        businessEventTypeObj.value = businessEventTypeObj.value.replace('_CODE', '');
        businessEventTypeObj.value = this.casePipe.transform(businessEventTypeObj.value, 'snake', 'space');
        businessEventTypeObj.value = this.capitalizePipe.transform(businessEventTypeObj.value, true);
        return businessEventTypeObj;
      });
  }

  public get filter(): UntypedFormControl {
    return this.form.get('filter') as UntypedFormControl;
  }

  ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      filter: [[], []]
    });
  }

  private initForm() {
    this.filter.valueChanges
      .pipe(
        debounceTime(750)
      )
      .subscribe((businessEventTypes: BusinessEventTypeEnum[]) =>
        this.getBusinessEvents.emit(businessEventTypes)
      );

    this.filter.patchValue(this.defaultFilters());
  }

  private defaultFilters(): BusinessEventTypeEnum[] {
    return EnumUtils.values(BusinessEventTypeEnum)
      .filter(v => v !== BusinessEventTypeEnum.ADMIN_VIEW_CODE);
  }

}
