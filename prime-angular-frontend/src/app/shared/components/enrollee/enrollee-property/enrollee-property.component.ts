import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-enrollee-property',
  templateUrl: './enrollee-property.component.html',
  styleUrls: ['./enrollee-property.component.scss']
})
export class EnrolleePropertyComponent implements OnInit {
  @Input() public title: string;
  @Input() public makeBold: boolean;
  @Input() public hasError: boolean;
  @Input() public errorMessage: string;

  constructor() { }

  public ngOnInit() { }
}
