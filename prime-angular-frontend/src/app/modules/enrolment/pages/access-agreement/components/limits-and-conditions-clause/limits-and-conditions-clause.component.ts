import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@enrolment/shared/models/terms-of-access.model';

@Component({
  selector: 'app-limits-and-conditions-clause',
  templateUrl: './limits-and-conditions-clause.component.html',
  styleUrls: ['./limits-and-conditions-clause.component.scss']
})
export class LimitsAndConditionsClauseComponent implements OnInit {
  @Input() limitsAndConditionsClauses: Clause[];

  constructor() { }

  public ngOnInit() { }
}
