import { Component, OnInit } from '@angular/core';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit {
  public SiteRoutes = SiteRoutes;

  constructor() { }

  public ngOnInit() { }
}
