import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

@Component({
  selector: 'app-site-registration-collection-notice',
  templateUrl: './site-registration-collection-notice.component.html',
  styleUrls: ['./site-registration-collection-notice.component.scss']
})
export class SiteRegistrationCollectionNoticeComponent implements OnInit {
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
