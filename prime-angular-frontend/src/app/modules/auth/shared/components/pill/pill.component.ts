import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-pill',
  templateUrl: './pill.component.html',
  styleUrls: ['./pill.component.scss']
})
export class PillComponent implements OnInit {
  @Input() public variant: number;
  constructor() {
    this.variant = 1;
  }

  ngOnInit() {
  }

}
