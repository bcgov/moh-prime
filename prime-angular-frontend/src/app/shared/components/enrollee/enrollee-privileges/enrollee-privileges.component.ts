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

  @Input() public transactions: Privilege[];
  @Input() public userType: Privilege;
  @Input() public canHaveOBOs: Privilege;

  public privilegeGroups: PrivilegeGroupConfig[];
  public privilegeTypes: Config<number>[];

  constructor(
    private configService: ConfigService
  ) {
    this.privileges = [];
    this.transactions = [];
    this.userType = null;
    this.canHaveOBOs = null;
    this.privilegeGroups = [];
    this.privilegeTypes = [];
  }

  public get transactionPrivileges() {
    if (this.privileges.length) {
      const tPrivileges: Privilege[] = [];
      for (const privilege of this.privileges) {
        if (this.privilegeGroups
          .filter(pg => pg.code === privilege.privilegeGroupCode)
          .filter(pg => pg.privilegeTypeCode === 2).length) {
          tPrivileges.push(privilege);
        }
      }
      return tPrivileges;
    }
    return this.transactions;
  }

  public get userTypePrivilege() {
    if (this.privileges.length) {
      for (const privilege of this.privileges) {
        if (privilege.privilegeGroupCode === 4) {
          return privilege;
        }
      }
    }
    return this.userType;
  }

  public get userTypeString() {
    if (this.userTypePrivilege.transactionType === 'RU') {
      return this.canHaveOBOsPrivilege
        ? 'You are allowed to have an "On Behalf User" access Pharmanet on your behalf'
        : 'You are not allowed to have an "On Behalf User" access Pharmanet on your behalf';
    } else {
      return 'You are allowed to access PharmaNet on behalf of a Regulated User';
    }
  }

  public get canHaveOBOsPrivilege() {
    if (this.privileges.length) {
      return this.privileges.filter(p => p.privilegeGroupCode === 5).length > 0;
    }
    return this.canHaveOBOs;
  }


  ngOnInit() {
    if (this.privileges.length) {
      this.privilegeGroups = this.configService ? this.configService.privilegeGroups : null;
    }
  }

}



