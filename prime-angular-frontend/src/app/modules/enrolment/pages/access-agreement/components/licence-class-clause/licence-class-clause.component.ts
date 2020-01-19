import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@enrolment/shared/models/access-term.model';

@Component({
  selector: 'app-licence-class-clause',
  templateUrl: './licence-class-clause.component.html',
  styleUrls: ['./licence-class-clause.component.scss']
})
export class LicenceClassClauseComponent implements OnInit {
  @Input() clauses: Clause[];

  constructor() { }

  public ngOnInit() { }
}
