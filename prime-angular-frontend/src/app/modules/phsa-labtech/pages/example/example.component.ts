import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-example',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.scss']
})
export class ExampleComponent implements OnInit {
  public title: string;

  constructor(
    private route: ActivatedRoute
  ) {
    this.title = this.route.snapshot.data.title;
  }

  public ngOnInit(): void { }
}
