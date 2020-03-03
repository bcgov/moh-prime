import { Component, OnInit, Input } from '@angular/core';

import { AccessTerm } from '@shared/models/access-term.model';

@Component({
  selector: 'app-ru-access-terms',
  templateUrl: './ru-access-terms.component.html',
  styleUrls: ['./ru-access-terms.component.scss']
})
export class RuAccessTermsComponent implements OnInit {
  @Input() public accessTerms: AccessTerm;

  constructor() { }

  public ngOnInit() { }
}
