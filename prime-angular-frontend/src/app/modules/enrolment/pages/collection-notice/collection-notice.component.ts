import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolent.routes';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  constructor() { }

  public EnrolmentRoutes = EnrolmentRoutes;

  public ngOnInit() {}
}
