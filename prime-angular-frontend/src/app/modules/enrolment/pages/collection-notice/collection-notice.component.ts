import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';


import { EnrolmentRoutes } from '@enrolment/enrolent.routes';


@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private router: Router) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  ngOnInit() {
  }

}
