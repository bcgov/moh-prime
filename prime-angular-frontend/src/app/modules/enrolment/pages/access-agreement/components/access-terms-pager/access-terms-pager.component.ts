import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';

import { Pager } from '../../pager';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';

@Component({
  selector: 'app-access-terms-pager',
  templateUrl: './access-terms-pager.component.html',
  styleUrls: ['./access-terms-pager.component.scss']
})
export class AccessTermsPagerComponent extends Pager implements OnInit {
  @Input() public accessTerm: AccessTerm;

  constructor(
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    super(changeDetectorRef);
  }

  public ngOnInit() { }
}
