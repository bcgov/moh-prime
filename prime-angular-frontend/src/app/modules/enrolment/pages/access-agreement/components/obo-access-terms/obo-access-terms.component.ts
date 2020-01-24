import { Component, OnInit, Input } from '@angular/core';

import { AccessTerm } from '@enrolment/shared/models/access-term.model';

@Component({
  selector: 'app-obo-access-terms',
  templateUrl: './obo-access-terms.component.html',
  styleUrls: ['./obo-access-terms.component.scss']
})
export class OboAccessTermsComponent implements OnInit {
  @Input() public accessTerms: AccessTerm;

  constructor() { }

  public ngOnInit() { }
}
