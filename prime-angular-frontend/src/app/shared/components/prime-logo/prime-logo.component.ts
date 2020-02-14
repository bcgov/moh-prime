import { Component, OnInit, Input } from '@angular/core';

type primeLogoFill = 'light' | 'dark';
type primeLogoMode = 'full' | 'icon';
type primeLogoPosition = 'none' | 'bottom' | 'right';
type primeLogoSize = 'small' | 'medium' | 'large';

interface PrimeLogoConfig {
  fill: primeLogoFill;
  mode: primeLogoMode;
  position: primeLogoPosition;
  size: primeLogoSize;
  dimensions: string;
  width: string;
  height: string;
}

@Component({
  selector: 'app-prime-logo',
  templateUrl: './prime-logo.component.svg',
  styleUrls: ['./prime-logo.component.scss']
})
export class PrimeLogoComponent implements OnInit {
  @Input() public fill: primeLogoFill;
  @Input() public mode: primeLogoMode;
  @Input() public position: primeLogoPosition;
  @Input() public size: primeLogoSize;

  public config: PrimeLogoConfig;

  constructor() {
    this.fill = 'dark';
    this.mode = 'full';
    this.position = 'bottom';
    this.size = 'medium';
  }

  public ngOnInit() {
    this.config = this.buildConfig(this.fill, this.mode, this.position, this.size);
  }

  private buildConfig(fill: primeLogoFill, mode: primeLogoMode, position: primeLogoPosition, size: primeLogoSize): PrimeLogoConfig {
    return (this.mode === 'icon')
      ? this.buildIconConfig(fill, mode, size)
      : this.buildFullConfig(fill, mode, size, position);
  }

  private buildFullConfig(fill: primeLogoFill, mode: primeLogoMode, size: primeLogoSize, position: primeLogoPosition): PrimeLogoConfig {
    const dimensions = (position === 'right')
      ? [0, 0, 200, 92]
      : [0, 0, 92, 142];
    return {
      mode,
      fill,
      size,
      position,
      dimensions: dimensions.join(','),
      width: `${dimensions[3]}`,
      height: `${dimensions[4]}`
    };
  }

  private buildIconConfig(fill: primeLogoFill, mode: primeLogoMode, size: primeLogoSize): PrimeLogoConfig {
    const position = 'none';
    const dimensions = [0, 0, 92, 92];
    return {
      mode,
      fill,
      size,
      position,
      dimensions: dimensions.join(','),
      width: `${dimensions[3]}`,
      height: `${dimensions[4]}`
    };
  }
}
