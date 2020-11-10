import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

import { Observable } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';
import { BusinessEventType } from '@adjudication/shared/models/business-event-type.model';
import { CasePipe } from '@shared/pipes/case.pipe';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

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
    this.businessEventTypes = EnumUtils.asObjects(BusinessEventType)
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

  public onMenuClose(businessEventTypes: BusinessEventType[]) {
    console.log('CLOSED', businessEventTypes);

    this.getBusinessEvents();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.getBusinessEvents();
  }

  public createFormInstance() {
    this.form = this.fb.group({
      filter: [null, []]
    });
  }

  public initForm() {
    // this.filter.valueChanges
    //   .pipe(
    //     debounceTime(500)
    //   )
    //   .subscribe((businessEventTypes: BusinessEventType[]) =>
    //     this.getBusinessEvents(businessEventTypes)
    //   );
  }

  private getBusinessEvents(businessEventTypes?: BusinessEventType[]) {
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
}
