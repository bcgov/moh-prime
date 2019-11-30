import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

import { Pager } from '../../pager';

@Component({
  selector: 'app-moa-access-agreement',
  templateUrl: './moa-access-agreement.component.html',
  styleUrls: ['./moa-access-agreement.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MoaAccessAgreementComponent extends Pager implements OnInit {
  constructor(
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    super(changeDetectorRef);
  }

  public ngOnInit() { }
}
