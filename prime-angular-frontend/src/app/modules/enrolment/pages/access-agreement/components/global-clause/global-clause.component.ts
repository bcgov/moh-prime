import { Component, OnInit, Input } from '@angular/core';
import { Clause } from '@shared/models/access-term.model';

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
