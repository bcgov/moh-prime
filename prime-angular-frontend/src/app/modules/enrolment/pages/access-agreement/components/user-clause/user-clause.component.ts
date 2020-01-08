import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@enrolment/shared/models/terms-of-access.model';

@Component({
  selector: 'app-user-clause',
  templateUrl: './user-clause.component.html',
  styleUrls: ['./user-clause.component.scss']
})
export class UserClauseComponent implements OnInit {
  @Input() clause: Clause;

  constructor() { }

  public ngOnInit() { }
}
