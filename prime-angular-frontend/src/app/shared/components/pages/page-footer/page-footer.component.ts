import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';

// TODO footer is too stringent make it more generic and wrap to apply logic
@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit, OnChanges {
  // TODO update not to use the word enrolment
  @Input() public isInitialEnrolment: boolean;
  @Input() public hasSecondaryAction: boolean;
  @Input() public disableSave: boolean;
  // TODO when refactored keep these in the generic page footer, and
  // drop saveButtonLabel and secondaryActionButtonLabel. Wrap to
  // enforce label defaults in specific modules
  @Input() public primaryActionLabel: string;
  @Input() public secondaryActionLabel: string;
  @Output() public save: EventEmitter<void>;
  @Output() public continue: EventEmitter<void>;
  @Output() public back: EventEmitter<void>;

  // TODO drop after generic component refactor
  public saveButtonLabel: string;
  public secondaryActionButtonLabel: string;

  constructor() {
    this.isInitialEnrolment = true;
    this.hasSecondaryAction = true;

    this.save = new EventEmitter<void>();
    this.continue = new EventEmitter<void>();
    this.back = new EventEmitter<void>();
  }

  // TODO change button labels based on input binding

  public onSave() {
    this.save.emit();
  }

  public onSecondaryAction() {
    (this.isInitialEnrolment)
      ? this.back.emit()
      : this.continue.emit();
  }

  public ngOnChanges(changes: SimpleChanges) {
    if (changes.primaryActionLabel) {
      this.saveButtonLabel = changes.primaryActionLabel.currentValue;
    }

    if (changes.secondaryActionLabel) {
      this.secondaryActionButtonLabel = changes.secondaryActionLabel.currentValue;
    }
  }

  public ngOnInit() {
    if (this.isInitialEnrolment) {
      // TODO temporary hack until time for generic page footer, otherwise normal functionality
      this.saveButtonLabel = this.primaryActionLabel ?? 'Save and Continue';
    } else {
      this.saveButtonLabel = this.primaryActionLabel ?? 'Continue';
      // TODO drop opinion from generic page footer
      this.hasSecondaryAction = (typeof this.hasSecondaryAction === 'boolean')
        ? this.hasSecondaryAction
        : false;
    }

    this.secondaryActionButtonLabel = this.secondaryActionLabel ?? 'Back';
  }
}
