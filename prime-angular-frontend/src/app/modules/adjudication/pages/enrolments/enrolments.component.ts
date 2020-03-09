import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-enrolments',
  templateUrl: './enrolments.component.html',
  styleUrls: ['./enrolments.component.scss']
})
export class EnrolmentsComponent implements OnInit {
  public title: string;
  public hasActions: boolean;

  constructor() {
    this.title = 'PRIME Enrollees';
    this.hasActions = false;
  }

  public ngOnInit() { }
}
