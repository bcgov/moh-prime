import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';

@Component({
  selector: 'app-contextual-site-notification',
  templateUrl: './contextual-site-notification.component.html',
  styleUrls: ['./contextual-site-notification.component.scss']
})
export class ContextualSiteNotificationComponent implements OnInit {
  @Input() public siteId: number;
  @Output() public removed: EventEmitter<number>;
  public notes: SiteRegistrationNote[];

  constructor(
    private adjudicationResource: AdjudicationResource,
  ) {
    this.removed = new EventEmitter<number>();
  }

  public onRemove(adjudicatorNoteId: number) {
    this.adjudicationResource.deleteSiteNotification(this.siteId, adjudicatorNoteId)
      .subscribe(() => this.removed.emit(this.siteId));
  }

  public ngOnInit(): void {
    this.getNotifications();
  }

  private getNotifications() {
    this.adjudicationResource.getNotificationsBySite(this.siteId)
      .subscribe(notes => this.notes = notes);
  }
}
