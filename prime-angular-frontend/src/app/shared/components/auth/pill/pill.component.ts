import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-pill',
    templateUrl: './pill.component.html',
    styleUrls: ['./pill.component.scss'],
    standalone: false
})
export class PillComponent implements OnInit {
  @Input() public mirror: boolean;

  constructor() {
    this.mirror = false;
  }

  public ngOnInit() { }
}
