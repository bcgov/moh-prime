import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentRoutes } from '@enrolment/enrolent.routes';


@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {

  public busy: Subscription;
  constructor(private route: ActivatedRoute) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  ngOnInit() {
  }

}
