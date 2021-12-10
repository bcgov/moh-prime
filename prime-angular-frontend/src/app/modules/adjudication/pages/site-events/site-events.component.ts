import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { SiteResource } from '@core/resources/site-resource.service';

@Component({
  selector: 'app-site-events',
  templateUrl: './site-events.component.html',
  styleUrls: ['./site-events.component.scss']
})
export class SiteEventsComponent implements OnInit {
  public businessEvents$: Observable<DateContent[]>;
  public hasActions: boolean;

  constructor(
    private route: ActivatedRoute,
    private siteResource: SiteResource,
  ) {
    this.hasActions = false;
  }

  public getBusinessEvents(businessEventTypes?: BusinessEventTypeEnum[]) {
    const siteId = +this.route.snapshot.params.sid;
    this.businessEvents$ = this.siteResource
      .getSiteBusinessEvents(siteId, businessEventTypes ?? [])
      .pipe(
        map((businessEvents: BusinessEvent[]) =>
          businessEvents.map((businessEvent: BusinessEvent) => {
            return {
              date: businessEvent.eventDate,
              content: businessEvent.description,
              name: businessEvent.adminIDIR,
              marginRight: businessEvent.partyName // Site Signing Authority
            };
          })
        )
      );
  }

  public ngOnInit() {
    this.getBusinessEvents();
  }
}
