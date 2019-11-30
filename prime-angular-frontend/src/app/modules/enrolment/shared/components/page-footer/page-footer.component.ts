import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';


@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit {

  @Input() backRoute: EnrolmentRoutes;
  @Input() hideBack: boolean;
  @Input() hasBackAction: boolean;

  @Output() handleBack: EventEmitter<void>;

  public justifyContent = 'between';

  constructor() {
    this.handleBack = new EventEmitter<void>();
  }

  public handleBackClick() {
    this.handleBack.emit();
  }

  public getJustifyContent(justifyContent: string) {
    return `justify-content${justifyContent}`;
  }

  public ngOnInit() {
    if (this.hideBack === true) {
      this.justifyContent = 'end';
    }
  }
}
