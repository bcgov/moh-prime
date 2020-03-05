import { Component, OnInit } from '@angular/core';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { HelpResource } from './services/help-resource.service';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss']
})
export class HelpComponent implements OnInit {
  public uniqueId: number;
  constructor(
    private helpResource: HelpResource,
  ) {
  }

  async ngOnInit() {
    await this.helpResource.enrolleeDisplayId().subscribe((id) => this.uniqueId = id);
  }

}
