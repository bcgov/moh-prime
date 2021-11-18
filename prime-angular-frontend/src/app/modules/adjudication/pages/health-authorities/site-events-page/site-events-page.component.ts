import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';

@Component({
  selector: 'app-site-events-page',
  templateUrl: './site-events-page.component.html',
  styleUrls: ['./site-events-page.component.scss']
})
export class SiteEventsPageComponent implements OnInit {
  public businessEvents$: Observable<DateContent[]>;

  constructor(
    private route: ActivatedRoute,
    private siteResource: SiteResource,
  ) { }

  public getBusinessEvents(businessEventTypes?: BusinessEventTypeEnum[]) {
    const siteId = this.route.snapshot.params.sid;
    this.businessEvents$ = this.siteResource
      .getSiteBusinessEvents(siteId, businessEventTypes ?? [])
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
