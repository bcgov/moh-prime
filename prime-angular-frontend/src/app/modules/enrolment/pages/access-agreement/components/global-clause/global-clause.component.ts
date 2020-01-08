import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@enrolment/shared/models/terms-of-access.model';

@Component({
  selector: 'app-global-clause',
  templateUrl: './global-clause.component.html',
  styleUrls: ['./global-clause.component.scss']
})
export class GlobalClauseComponent implements OnInit {
  @Input() clause: Clause;

  constructor() { }

  public ngOnInit() { }
}
