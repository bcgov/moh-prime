import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public hasActions: boolean;

  constructor() {
    this.hasActions = false;
  }

  public ngOnInit() { }
}
