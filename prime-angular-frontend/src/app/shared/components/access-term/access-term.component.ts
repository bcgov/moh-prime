import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

import { EnrolleeAgreement } from '@shared/models/agreement.model';

@Component({
  selector: 'app-access-term',
  templateUrl: './access-term.component.html',
  styleUrls: ['./access-term.component.scss']
})
export class AccessTermComponent implements OnInit, OnChanges {
  @Input() public accessTerms: EnrolleeAgreement;

  public agreementContent: string;

  constructor() { }

  public ngOnChanges(change: SimpleChanges) {
    if (change.accessTerms.currentValue) {
      this.agreementContent = this.accessTerms.agreementContent;
    }
  }

  public ngOnInit() { }
}
