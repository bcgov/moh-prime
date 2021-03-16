import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-contextual-enrollee-notification',
  templateUrl: './contextual-enrollee-notification.component.html',
  styleUrls: ['./contextual-enrollee-notification.component.scss']
})
export class ContextualEnrolleeNotificationComponent implements OnInit {
  @Input() public enrolleeId: number;
  @Output() public removed: EventEmitter<number>;
  public notes: EnrolleeNote[];

  constructor(
    private adjudicationResource: AdjudicationResource,
  ) {
    this.removed = new EventEmitter<number>();
  }

  public onRemove(adjudicatorNoteId: number) {
    this.adjudicationResource.deleteEnrolleeNotification(this.enrolleeId, adjudicatorNoteId)
      .subscribe(() => this.removed.emit(this.enrolleeId));
  }

  public ngOnInit(): void {
    this.getEscalatedNotes();
  }

  private getEscalatedNotes() {
    this.adjudicationResource.getNotificationsByEnrollee(this.enrolleeId)
      .subscribe(notes => this.notes = notes);
  }
}
