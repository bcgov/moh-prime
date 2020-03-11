import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-technical-support-contact',
  templateUrl: './technical-support-contact.component.html',
  styleUrls: ['./technical-support-contact.component.scss']
})
export class TechnicalSupportContactComponent implements OnInit {
  form: FormGroup;
  public hasSeparateAddress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
  ) { }

  ngOnInit() {

  }

  onSubmit() {
    this.router.navigate([SiteRoutes.SITE_REVIEW], { relativeTo: this.route.parent });
  }

  onBack() {
    this.router.navigate([SiteRoutes.PRIVACY_OFFICER], { relativeTo: this.route.parent });
  }
}
