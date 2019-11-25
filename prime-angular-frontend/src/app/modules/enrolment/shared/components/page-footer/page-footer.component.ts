import { Component, OnInit, Input } from '@angular/core';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';

@Component({
  selector: 'app-page-footer',
  templateUrl: './page-footer.component.html',
  styleUrls: ['./page-footer.component.scss']
})
export class PageFooterComponent implements OnInit {

  @Input() backRoute: EnrolmentRoutes;
  @Input() onClick;
  @Input() hideBack: boolean;

  public justifyContent = 'between';

  constructor() { }

  public getJustifyContent(justifyContent: string) {
    return `justify-content${justifyContent}`;
  }

  ngOnInit() {
    console.log(this.hideBack);
    if (this.hideBack === true) {
      this.justifyContent = 'end';
    }
  }



}
