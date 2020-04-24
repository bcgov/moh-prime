import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-enrollee-page',
  templateUrl: './enrollee-page.component.html',
  styleUrls: ['./enrollee-page.component.scss']
})
export class EnrolleePageComponent implements OnInit {
  @Input() public busy: Subscription;
  @Input() public mode: 'default' | 'full';

  constructor() {
    this.mode = 'default';
  }

  public ngOnInit() { }
}
