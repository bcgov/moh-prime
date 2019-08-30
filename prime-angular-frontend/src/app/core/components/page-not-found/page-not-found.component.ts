import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.scss']
})
export class PageNotFoundComponent implements OnInit {
  public phrase: string;
  private phrases: string[];

  constructor(
    private location: Location,
    private router: Router
  ) {
    this.phrases = [
      'There are mysteries to the universe we were never meant to solve, but why you\'re here, is not among them.'
    ];
  }

  public ngOnInit() {
    this.setErrorPhrase();
  }

  private setErrorPhrase() {
    const max = Math.floor(this.phrases.length - 1);
    const ndx = Math.floor(Math.random() * (max + 1));

    this.phrase = this.phrases[ndx];
  }
}
