import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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
  public businessEvents$: Observable<DateContent[]>;
  public hasActions: boolean;

  private businessEventTypes: BusinessEventTypeEnum[];

  constructor(
    private route: ActivatedRoute,
    private adjucationResource: AdjudicationResource,
  ) {
    this.hasActions = true;
    this.businessEventTypes = [];
  }

  public getBusinessEvents(businessEventTypes?: BusinessEventTypeEnum[]) {
    if (businessEventTypes !== undefined) {
      this.businessEventTypes = businessEventTypes;
    }
    const enrolleeId = this.route.snapshot.params.id;
    this.businessEvents$ = this.adjucationResource
      .getEnrolleeBusinessEvents(enrolleeId, this.businessEventTypes)
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

  public ngOnInit() {
    this.getBusinessEvents();
  }
}
