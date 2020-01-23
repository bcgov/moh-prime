import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-prime-contact',
  templateUrl: './prime-contact.component.html',
  styleUrls: ['./prime-contact.component.scss']
})
export class PrimeContactComponent {
  @Input() showPhone: boolean;
  @Input() showEmail: boolean;

  constructor() {
    this.showPhone = true;
    this.showEmail = true;
  }
}
