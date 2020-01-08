import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';

import { Pager } from '../../pager';
import { TermsOfAccess } from '@enrolment/shared/models/terms-of-access.model';

@Component({
  selector: 'app-terms-of-access-pager',
  templateUrl: './terms-of-access-pager.component.html',
  styleUrls: ['./terms-of-access-pager.component.scss']
})
export class TermsOfAccessPagerComponent extends Pager implements OnInit {
  @Input() termsOfAccess: TermsOfAccess;

  constructor(
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    super(changeDetectorRef);
  }

  public ngOnInit() { }
}
