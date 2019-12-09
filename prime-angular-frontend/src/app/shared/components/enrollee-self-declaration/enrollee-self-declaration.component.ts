import { Component, ChangeDetectionStrategy, Input } from '@angular/core';

@Component({
  selector: 'app-enrollee-self-declaration',
  templateUrl: './enrollee-self-declaration.component.html',
  styleUrls: ['./enrollee-self-declaration.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeSelfDeclarationComponent {
  @Input() public hasDeclaration: boolean;
  @Input() public details: string;

  constructor() {
    this.hasDeclaration = false;
  }
}
