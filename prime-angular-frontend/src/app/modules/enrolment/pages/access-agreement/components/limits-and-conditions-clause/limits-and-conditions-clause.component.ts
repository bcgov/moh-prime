import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@enrolment/shared/models/access-term.model';

@Component({
  selector: 'app-limits-and-conditions-clause',
  templateUrl: './limits-and-conditions-clause.component.html',
  styleUrls: ['./limits-and-conditions-clause.component.scss']
})
export class LimitsAndConditionsClauseComponent implements OnInit {
  @Input() clause: Clause;

  constructor() { }

  public ngOnInit() { }
}
