import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-infographic',
  templateUrl: './infographic.component.html',
  styleUrls: ['./infographic.component.scss']
})
export class InfographicComponent implements OnInit {
  @Input() public imageName: string;

  constructor() { }

  public getClass(): string {
    return `${this.imageName} info`;
  }

  ngOnInit(): void {
  }

}
