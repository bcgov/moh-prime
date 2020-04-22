import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

// TODO footer is too stringent make it more generic and wrap to apply logic
@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit {
  // TODO update not to use the word enrolment
  @Input() public isInitialEnrolment: boolean;
  @Input() public hasSecondaryAction: boolean;
  @Input() public disableSave: boolean;
  @Output() public save: EventEmitter<void>;
  @Output() public continue: EventEmitter<void>;
  @Output() public back: EventEmitter<void>;

  public saveButtonLabel: string;
  public secondaryActionButtonLabel: string;

  constructor() {
    this.isInitialEnrolment = true;
    this.hasSecondaryAction = true;

    this.save = new EventEmitter<void>();
    this.continue = new EventEmitter<void>();
    this.back = new EventEmitter<void>();
  }

  public onSave() {
    this.save.emit();
  }

  public onSecondaryAction() {
    (this.isInitialEnrolment)
      ? this.back.emit()
      : this.continue.emit();
  }

  public ngOnInit() {
    if (this.isInitialEnrolment) {
      this.saveButtonLabel = 'Save and Continue';
      this.secondaryActionButtonLabel = 'Back';
    } else {
      this.saveButtonLabel = 'Continue';
      this.hasSecondaryAction = false;
    }
  }
}
