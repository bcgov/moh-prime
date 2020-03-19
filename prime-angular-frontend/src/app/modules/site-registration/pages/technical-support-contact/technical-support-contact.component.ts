import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-technical-support-contact',
  templateUrl: './technical-support-contact.component.html',
  styleUrls: ['./technical-support-contact.component.scss']
})
export class TechnicalSupportContactComponent implements OnInit {
  public form: FormGroup;
  public hasSeparateAddress: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onSubmit() {
    this.router.navigate([SiteRoutes.SITE_REVIEW], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.PRIVACY_OFFICER], { relativeTo: this.route.parent });
  }
  public ngOnInit() { }
}
