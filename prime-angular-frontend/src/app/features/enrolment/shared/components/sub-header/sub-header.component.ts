import { Component, OnInit, Input, Optional } from '@angular/core';

@Component({
  selector: 'app-sub-header',
  templateUrl: './sub-header.component.html',
  styleUrls: ['./sub-header.component.scss']
})
export class SubHeaderComponent implements OnInit {
  @Input() public subheader: string;
  @Input() public help: string;

  constructor() { }

  public ngOnInit() { }
}
