import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';

type primeLogoFill = 'light' | 'dark';
type primeLogoLabel = 'none' | 'bottom' | 'right' | 'left';
type primeLogoSize = 'small' | 'medium' | 'large';

interface PrimeLogoConfig {
  fill: primeLogoFill;
  label: primeLogoLabel;
  viewbox: string;
  width: string;
  height: string;
}

@Component({
  selector: 'app-prime-logo',
  templateUrl: './prime-logo.component.svg',
  styleUrls: ['./prime-logo.component.scss']
})
export class PrimeLogoComponent implements OnInit, OnChanges {
  @Input() public fill: primeLogoFill;
  @Input() public label: primeLogoLabel;
  @Input() public size: primeLogoSize;

  public config: PrimeLogoConfig;

  private scale: { [key: string]: number };

  constructor() {
    this.fill = 'dark';
    this.label = 'bottom';
    this.size = 'medium';

    this.scale = {
      small: 0.75,
      medium: 1,
      large: 2
    };
  }

  public ngOnChanges(changes: SimpleChanges) {
    if (changes.size.currentValue) {
      this.getConfig();
    }
  }

  public ngOnInit() {
    this.getConfig();
  }

  private getConfig() {
    this.config = {
      fill: this.fill,
      ...this.buildConfig(this.label, this.size)
    };
  }

  private buildConfig(label: primeLogoLabel, size: primeLogoSize): PrimeLogoConfig {
    const viewbox = this.getViewbox(label);
    const dimensions = this.getDimensions(size, viewbox);
    return {
      label,
      viewbox: viewbox.join(','),
      ...dimensions
    } as PrimeLogoConfig;
  }

  private getViewbox(label: primeLogoLabel): number[] {
    const vbCoords = [-2, -2]; // viewbox top/left padding
    const vbDimensions = (label === 'none')
      ? [100, 100]
      : (label === 'right')
        ? [212, 100]
        : (label === 'left')
          ? [212, 100]
          : [100, 150];

    return [...vbCoords, ...vbDimensions];
  }

  private getDimensions(size: primeLogoSize, viewbox: number[]): Partial<PrimeLogoConfig> {
    return {
      width: `${viewbox[2] * this.scale[size]}px`,
      height: `${viewbox[3] * this.scale[size]}px`
    };
  }
}
