import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-enrolment-collection-notice',
  templateUrl: './enrolment-collection-notice.component.html',
  styleUrls: ['./enrolment-collection-notice.component.scss']
})
export class EnrolmentCollectionNoticeComponent implements OnInit {
  @Input() public showAlert: boolean;
  @Output() public accepted: EventEmitter<void>;

  constructor() {
    this.accepted = new EventEmitter<void>();
    // Default to use alert versus full page
    this.showAlert = true;
  }

  public onAccept() {
    this.accepted.emit();
  }

  public ngOnInit(): void { }
}
