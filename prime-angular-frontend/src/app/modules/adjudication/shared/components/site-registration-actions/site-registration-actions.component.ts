import { Component, OnInit, Input } from '@angular/core';

import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() site: Site;

  constructor() { }

  public ngOnInit(): void { }
}
