import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-enrollee-property-error',
  templateUrl: './enrollee-property-error.component.html',
  styleUrls: ['./enrollee-property-error.component.scss']
})
export class EnrolleePropertyErrorComponent implements OnInit {
  @Input() public message: string;

  constructor() {
    this.message = 'Error';
  }

  ngOnInit() {
  }

}
