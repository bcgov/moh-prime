import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.scss']
})
export class AdministratorComponent implements OnInit {
  public form: FormGroup;
  public hasSeparateAddress: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onSubmit() {
    this.router.navigate([SiteRoutes.PRIVACY_OFFICER], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.SIGNING_AUTHORITY], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }

}
