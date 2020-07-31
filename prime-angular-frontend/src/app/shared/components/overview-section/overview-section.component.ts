import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-overview-section',
  templateUrl: './overview-section.component.html',
  styleUrls: ['./overview-section.component.scss']
})
export class OverviewSectionComponent implements OnInit {
  @Input() public title: string;
  @Input() public tooltip: string;
  @Input() public editRoute: string;
  @Input() public showEditRedirect: boolean;
  @Output() public route: EventEmitter<string>;

  constructor() {
    this.route = new EventEmitter<string>();
  }

  public ngOnInit(): void { }

  public onRoute(routePath: string): void {
    this.route.emit(routePath);
  }
}
