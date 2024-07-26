import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Admin, AdminUser } from '@auth/shared/models/admin.model';
import { AdminStatusType } from '@adjudication/shared/models/admin-status.enum';
import { Role } from '@auth/shared/enum/role.enum';

@Component({
  selector: 'app-admin-users-table',
  templateUrl: './admin-users-table.component.html',
  styleUrls: ['./admin-users-table.component.scss']
})
export class AdminUsersTableComponent implements OnInit {

  public dataSource: AdminUser[];
  public displayColumns: string[] = ['username', 'firstname', 'lastname', 'siteassigned', 'enrolleeassigned', 'status'];
  public AdminStatus: AdminStatusType;
  public Role = Role;

  constructor(
    private adjudicationResource: AdjudicationResource,
  ) {
  }

  public toggleStatus(adminId: number, change: MatSlideToggleChange) {
    if (change.checked) {
      this.adjudicationResource.enableAdmin(adminId).subscribe((admin: Admin) => {
        this.dataSource.find(admin => admin.id === adminId).status = admin.status;
      });
    } else {
      this.adjudicationResource.disableAdmin(adminId).subscribe((admin: Admin) => {
        this.dataSource.find(admin => admin.id === adminId).status = admin.status;
      });
    }
  }

  public ngOnInit(): void {
    this.getAdjudicators();
  }

  private getAdjudicators(): void {
    this.adjudicationResource.getAdminUsers()
      .subscribe((adminUsers: AdminUser[]) => this.dataSource = adminUsers);
  }

  public isEnabled(status: number) {
    return status === AdminStatusType.ENABLED;
  }
}
