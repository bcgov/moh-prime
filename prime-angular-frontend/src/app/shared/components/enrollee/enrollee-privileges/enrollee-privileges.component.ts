import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { Privilege } from '@enrolment/shared/models/privilege.model';
import { PrivilegeGroupConfig, Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-enrollee-privileges',
  templateUrl: './enrollee-privileges.component.html',
  styleUrls: ['./enrollee-privileges.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleePrivilegesComponent implements OnInit {
  @Input() public header: string;
  @Input() public privileges: Privilege[];

  public privilegeGroups: PrivilegeGroupConfig[];
  public privilegeTypes: Config<number>[];


  constructor(
    private configService: ConfigService
  ) {
    this.privilegeGroups = this.configService.privilegeGroups;
  }

  public get transactionPrivileges() {
    // const tPrivileges: Privilege[] = [];
    // for (const privilege of this.privileges) {
    //   if (this.privilegeGroups
    //     .filter(pg => pg.code === privilege.privilegeGroupCode)
    //     .filter(pg => pg.privilegeTypeCode === 2).length !== 0) {
    //     tPrivileges.push(privilege);
    //   }
    // }

    const tPrivileges: Privilege[] = [];
    this.privileges.map(privilege => {
      if (this.privilegeGroups
        .filter(pg => pg.code === privilege.privilegeGroupCode)
        .filter(pg => pg.privilegeTypeCode === 2).length !== 0) {
        tPrivileges.push(privilege);
      }
    });
    // return tPrivileges;
    return tPrivileges;
  }

  private setRolePrivileges() {
    for (const privilege of this.privileges) {
      if (this.privilegeGroups
        .filter(pg => pg.code === privilege.privilegeGroupCode)
        .filter(pg => pg.privilegeTypeCode === 2).length !== 0) {
        this.transactionPrivileges.push(privilege);
      }
    }
  }


  ngOnInit() {
  }

}



