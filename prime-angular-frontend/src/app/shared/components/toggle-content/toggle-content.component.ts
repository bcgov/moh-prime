import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-toggle-content',
  templateUrl: './toggle-content.component.html',
  styleUrls: ['./toggle-content.component.scss']
})
export class ToggleContentComponent implements OnInit {
  @Input() public color: 'primary' | 'accent' | 'warn';
  @Input() public label: string;
  @Input() public checked: boolean;
  @Output() public toggle: EventEmitter<Pick<MatSlideToggleChange, 'checked'>>;

  constructor() {
    this.color = 'primary';
    this.toggle = new EventEmitter<Pick<MatSlideToggleChange, 'checked'>>();
  }

  public onChange({ checked }: MatSlideToggleChange): void {
    this.checked = !this.checked;
    this.toggle.emit({ checked });
  }

  public ngOnInit(): void { }
}
