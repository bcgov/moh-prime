import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';

import { Pager } from '../../pager';

@Component({
  selector: 'app-ru-access-agreement',
  templateUrl: './ru-access-agreement.component.html',
  styleUrls: ['./ru-access-agreement.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RuAccessAgreementComponent extends Pager implements OnInit {
  constructor(
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    super(changeDetectorRef);
  }

  public ngOnInit() { }
}
