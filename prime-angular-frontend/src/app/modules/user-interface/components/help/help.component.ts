import { Component, OnInit } from '@angular/core';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
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
  ) {
  }

  async ngOnInit() {
    await this.helpResource.enrolleeDisplayId().subscribe(
      (help) => {
        const num = Math.floor(Math.random() * 1000000);
        this.helpIdentifier = `${help}-${num}`;
      }
    );
  }

}
