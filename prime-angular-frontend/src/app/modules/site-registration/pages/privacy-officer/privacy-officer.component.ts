import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-privacy-officer',
  templateUrl: './privacy-officer.component.html',
  styleUrls: ['./privacy-officer.component.scss']
})
export class PrivacyOfficerComponent implements OnInit {
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
    this.router.navigate([SiteRoutes.TECHNICAL_SUPPORT_CONTACT], { relativeTo: this.route.parent });
  }

  onBack() {
    this.router.navigate([SiteRoutes.ADMINISTRATOR], { relativeTo: this.route.parent });
  }

}
