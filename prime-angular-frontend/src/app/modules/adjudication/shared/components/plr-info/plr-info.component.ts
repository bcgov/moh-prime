import { Component, Input, OnInit } from '@angular/core';

import { PlrInfo } from '@adjudication/shared/models/plr-info.model';

@Component({
  selector: 'app-plr-info',
  templateUrl: './plr-info.component.html',
  styleUrls: ['./plr-info.component.scss']
})
export class PlrInfoComponent implements OnInit {
  @Input() public plrData: PlrInfo;

  constructor() { }

  public ngOnInit(): void { }
}
