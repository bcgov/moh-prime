import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-enrollee-events',
  templateUrl: './enrollee-events.component.html',
  styleUrls: ['./enrollee-events.component.scss']
})
export class EnrolleeEventsComponent implements OnInit {
  public hasActions: boolean;

  constructor() {
    this.hasActions = false;
  }

  public ngOnInit() { }
}
