import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-escalation-contextual',
  templateUrl: './escalation-contextual.component.html',
  styleUrls: ['./escalation-contextual.component.scss']
})
export class EscalationContextualComponent implements OnInit {
  @Input() public enrolleeId: number;
  @Input() public adjudicatorNoteId: number;
  @Output() public removed: EventEmitter<boolean>;
  public note: EnrolleeNote;

  constructor(
    private adjudicationResource: AdjudicationResource,
  ) {
    this.removed = new EventEmitter<boolean>();
  }

  public onRemove() {
    this.adjudicationResource.deleteEnrolmentEscalation(this.enrolleeId, this.adjudicatorNoteId)
      .subscribe(() => this.removed.emit(true));
  }

  public ngOnInit(): void {
    this.getEscalatedNote();
  }

  private getEscalatedNote() {
    this.adjudicationResource.getEscalatedNote(this.enrolleeId, this.adjudicatorNoteId)
      .subscribe(note => this.note = note);
  }

}
