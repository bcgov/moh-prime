import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-metabase-reports',
  templateUrl: './metabase-reports.component.html',
  styleUrls: ['./metabase-reports.component.scss']
})
export class MetabaseReportsComponent implements OnInit {
  public metabaseUrl: string;

  constructor(
    private adjudicationResource: AdjudicationResource,
  ) { }

  public ngOnInit(): void {
    this.getMetabaseToken();
    console.log(this.metabaseUrl);
  }

  private getMetabaseToken(): void {
    this.adjudicationResource.getMetabaseToken().subscribe((token: string) => this.metabaseUrl = token);
  }
}
