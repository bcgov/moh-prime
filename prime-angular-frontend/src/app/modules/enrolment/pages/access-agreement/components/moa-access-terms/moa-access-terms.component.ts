import { Component, OnInit, Input } from '@angular/core';

import { AccessTerm } from '@enrolment/shared/models/access-term.model';

@Component({
  selector: 'app-moa-access-terms',
  templateUrl: './moa-access-terms.component.html',
  styleUrls: ['./moa-access-terms.component.scss']
})
export class MoaAccessTermsComponent implements OnInit {
  @Input() public accessTerms: AccessTerm;

  constructor() { }

  public ngOnInit() { }
}
