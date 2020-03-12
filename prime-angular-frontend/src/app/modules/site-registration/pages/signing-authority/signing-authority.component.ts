import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-signing-authority',
  templateUrl: './signing-authority.component.html',
  styleUrls: ['./signing-authority.component.scss']
})
export class SigningAuthorityComponent implements OnInit {
  public form: FormGroup;
  public hasSeparateAddress: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onSubmit() {
    this.router.navigate([SiteRoutes.ADMINISTRATOR], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }

}
