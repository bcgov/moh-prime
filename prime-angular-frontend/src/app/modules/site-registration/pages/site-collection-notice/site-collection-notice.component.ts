import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-site-collection-notice',
  templateUrl: './site-collection-notice.component.html',
  styleUrls: ['./site-collection-notice.component.scss']
})
export class SiteCollectionNoticeComponent implements OnInit {

  constructor(
    private authService: AuthService,
  ) { }

  ngOnInit() {
  }

}
