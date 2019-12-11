import { Component, OnInit, Input } from '@angular/core';

import { ToastService } from '@core/services/toast.service';

@Component({
  selector: 'app-clipboard-icon',
  templateUrl: './clipboard-icon.component.html',
  styleUrls: ['./clipboard-icon.component.scss']
})
export class ClipboardIconComponent implements OnInit {
  @Input() public iconOnly: boolean;
  @Input() public content: string;

  constructor(
    private toastService: ToastService
  ) {
    this.iconOnly = true;
  }

  public ngOnInit() { }

  public onCopy() {
    this.toastService.openSuccessToast('Copied to Clipboard');
  }
}
