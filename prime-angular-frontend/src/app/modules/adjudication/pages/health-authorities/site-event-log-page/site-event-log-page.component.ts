import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';
import { SiteResource } from '@core/resources/site-resource.service';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';

@Component({
  selector: 'app-site-event-log-page',
  templateUrl: './site-event-log-page.component.html',
  styleUrls: ['./site-event-log-page.component.scss']
})
export class SiteEventLogPageComponent implements OnInit {
  public businessEvents$: Observable<DateContent[]>;
  public hasActions: boolean;

  constructor(
    private route: ActivatedRoute,
    private siteResource: SiteResource
  ) {
    this.hasActions = false;
  }

  public getBusinessEvents(businessEventTypes?: BusinessEventTypeEnum[]) {
    const enrolleeId = this.route.snapshot.params.sid;
    this.businessEvents$ = this.siteResource
      .getSiteBusinessEvents(enrolleeId, businessEventTypes ?? [])
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

  public ngOnInit(): void {
    this.getBusinessEvents();
  }
}
