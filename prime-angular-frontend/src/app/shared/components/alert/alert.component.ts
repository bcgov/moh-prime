import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit {

  @Input() text: string;
  @Output() myEvent: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

}
