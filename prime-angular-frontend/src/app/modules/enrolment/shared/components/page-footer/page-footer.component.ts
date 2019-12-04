import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Enrolment } from '@shared/models/enrolment.model';


@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit {
  @Input() hasInitialStatus: boolean;
  @Input() hasSecondaryAction: boolean;
  @Input() disableSave: boolean;
  @Output() save: EventEmitter<void>;
  @Output() continue: EventEmitter<void>;
  @Output() back: EventEmitter<void>;

  public saveButtonLabel: string;
  public secondaryActionButtonLabel: string;

  constructor() {
    this.hasInitialStatus = false;
    this.hasSecondaryAction = true;

    this.save = new EventEmitter<void>();
    this.continue = new EventEmitter<void>();
    this.back = new EventEmitter<void>();
  }

  public onSave() {
    this.save.emit();
  }

  public onSecondaryAction() {
    (this.hasInitialStatus)
      ? this.back.emit()
      : this.continue.emit();
  }

  public ngOnInit() {
    if (this.hasInitialStatus) {
      this.saveButtonLabel = 'Save and Continue';
      this.secondaryActionButtonLabel = 'Back';
    } else {
      this.saveButtonLabel = 'Save and Submit';
      this.secondaryActionButtonLabel = 'Save and Continue Editing';
    }
  }
}
