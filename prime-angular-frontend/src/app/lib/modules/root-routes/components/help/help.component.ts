import { Component, OnInit } from '@angular/core';

import { HelpResource } from './services/help-resource.service';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss']
})
export class HelpComponent implements OnInit {
  public helpIdentifier: string;

  constructor(
    private helpResource: HelpResource,
  ) { }

  public ngOnInit() {
    this.helpResource.enrolleeDisplayId()
      .subscribe((help: number) => {
        const num = Math.floor(Math.random() * 1000000);
        this.helpIdentifier = `${help}-${num}`;
      });
  }
}
