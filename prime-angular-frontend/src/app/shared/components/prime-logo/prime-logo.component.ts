import { Component, OnInit, Input } from '@angular/core';

type primeLogoMode = 'full' | 'icon';
type primeLogoFill = 'light' | 'dark';
type primeLogoPosition = 'none' | 'bottom' | 'right';
type primeLogoSize = 'small' | 'medium' | 'large';

@Component({
  selector: 'app-prime-logo',
  templateUrl: './prime-logo.component.svg',
  styleUrls: ['./prime-logo.component.scss']
})
export class PrimeLogoComponent implements OnInit {
  @Input() public mode: primeLogoMode;
  @Input() public size: primeLogoSize;
  @Input() public fill: primeLogoFill;
  @Input() public position: primeLogoPosition;

  public config: { [key: string]: any };

  constructor() {
    this.mode = 'full';
    this.size = 'medium';
    this.fill = 'dark';
    this.position = 'bottom';
  }

  public ngOnInit() {
    this.config = this.buildConfig(this.mode, this.size, this.fill, this.position);
  }

  private buildConfig(mode: primeLogoMode, size: primeLogoSize, fill: primeLogoFill, position: primeLogoPosition) {
    return (this.mode === 'icon')
      ? this.buildIconConfig(mode, size, fill)
      : this.buildFullConfig(mode, size, fill, position);
  }

  private buildFullConfig(mode: primeLogoMode, size: primeLogoSize, fill: primeLogoFill, position: primeLogoPosition) {
    const dimensions = (position === 'right')
      ? '0 0 200 100'
      : '0 0 100 150';

    return { mode, fill, size, position, dimensions };
  }

  private buildIconConfig(mode: primeLogoMode, size: primeLogoSize, fill: primeLogoFill) {
    const dimensions = '0 0 100 100';
    return { mode, fill, size, position: 'none', dimensions };
  }
}
