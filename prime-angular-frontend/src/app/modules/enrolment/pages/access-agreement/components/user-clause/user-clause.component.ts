import { Component, OnInit, Input } from '@angular/core';
import { UserClause } from '@shared/models/access-term.model';

@Component({
  selector: 'app-user-clause',
  templateUrl: './user-clause.component.html',
  styleUrls: ['./user-clause.component.scss']
})
export class UserClauseComponent implements OnInit {
  @Input() clause: UserClause;

  constructor() { }

  public ngOnInit() { }
}
