import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

import { Observable } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { CasePipe } from '@shared/pipes/case.pipe';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';

@Component({
  selector: 'app-enrollee-events',
  templateUrl: './enrollee-events.component.html',
  styleUrls: ['./enrollee-events.component.scss']
})
export class EnrolleeEventsComponent implements OnInit {
  public form: FormGroup;
  public hasActions: boolean;
  public businessEvents$: Observable<DateContent[]>;
  public businessEventTypes: { key: number, value: string }[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private adjucationResource: AdjudicationResource,
    private casePipe: CasePipe,
    private capitalizePipe: CapitalizePipe
  ) {
    this.hasActions = true;
    this.businessEventTypes = EnumUtils.asObjects(BusinessEventTypeEnum)
      .map((businessEventTypeObj: { key: number, value: string }) => {
        businessEventTypeObj.value = businessEventTypeObj.value.replace('_CODE', '');
        businessEventTypeObj.value = this.casePipe.transform(businessEventTypeObj.value, 'snake', 'space');
        businessEventTypeObj.value = this.capitalizePipe.transform(businessEventTypeObj.value, true);
        return businessEventTypeObj;
      });
  }

  public get filter(): FormControl {
    return this.form.get('filter') as FormControl;
  }

  public onAction() {
    this.getBusinessEvents();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  public createFormInstance() {
    this.form = this.fb.group({
      filter: [[], []]
    });
  }

  public initForm() {
    this.filter.valueChanges
      .pipe(
        debounceTime(750)
      )
      .subscribe((businessEventTypes: BusinessEventTypeEnum[]) =>
        this.getBusinessEvents(businessEventTypes)
      );

    this.filter.patchValue(this.defaultFilters());
  }

  private getBusinessEvents(businessEventTypes?: BusinessEventTypeEnum[]) {
    const enrolleeId = this.route.snapshot.params.id;
    this.businessEvents$ = this.adjucationResource
      .getEnrolleeBusinessEvents(enrolleeId, businessEventTypes ?? [])
      .pipe(
        map((businessEvents: BusinessEvent[]) =>
          businessEvents.map((businessEvent: BusinessEvent) => {
            return {
              date: businessEvent.eventDate,
              content: businessEvent.description,
              name: businessEvent.adminIDIR
            };
          })
        )
      );
  }

  private defaultFilters(): BusinessEventTypeEnum[] {
    return EnumUtils.values(BusinessEventTypeEnum)
      .filter(v => v !== BusinessEventTypeEnum.ADMIN_VIEW_CODE);
  }
}
