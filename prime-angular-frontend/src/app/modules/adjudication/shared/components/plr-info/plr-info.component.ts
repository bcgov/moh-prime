import { Component, Input, OnInit } from '@angular/core';

class PlrInfo {
  id: number;
  identifierType: string;
  collegeId: string;
  providerRoleType: string;
  namePrefix: string;
  firstName: string;
  secondName: string;
  thirdName: string;
  lastName: string;
  statusCode: string;
  statusReasonCode: string;
  expertise: string[];
  updatedTimeStamp: string;
  statusStartDate: string;
}

@Component({
  selector: 'app-plr-info',
  templateUrl: './plr-info.component.html',
  styleUrls: ['./plr-info.component.scss']
})
export class PlrInfoComponent implements OnInit {
  @Input() public plrData: PlrInfo;

  constructor() { }

  public ngOnInit(): void {
  }
}
