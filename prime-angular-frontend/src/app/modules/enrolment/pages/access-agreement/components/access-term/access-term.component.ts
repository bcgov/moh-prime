import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';

const PLACEHOLDER = '{$lcPlaceholder}';
const PREFIX = '<li><p class="bold underline">Limits and Conditions</p>';
const SUFFIX = '</li>';

@Component({
  selector: 'app-access-term',
  templateUrl: './access-term.component.html',
  styleUrls: ['./access-term.component.scss']
})
export class AccessTermComponent implements OnInit {
  @Input() public accessTerms: AccessTerm;
  public clause: string;

  constructor() { }

  ngOnInit() {
    if (this.accessTerms) {
      const tempClause: string = this.accessTerms.userClause.clause;
      const limits: string = this.accessTerms.limitsConditionsClause.clause;
      const content = limits == null ? '' : PREFIX + limits + SUFFIX;
      this.clause = tempClause.replace(PLACEHOLDER, content);
    }
  }

}
