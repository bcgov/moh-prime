import { Component, OnInit, Input } from '@angular/core';
import { ToastService } from '@core/services/toast.service';

@Component({
  selector: 'app-clipboard-icon',
  templateUrl: './clipboard-icon.component.html',
  styleUrls: ['./clipboard-icon.component.scss']
})
export class ClipboardIconComponent implements OnInit {
  @Input() public message: string;

  constructor(
    private toastService: ToastService
  ) { }

  ngOnInit() {
  }

  public copyToast() {
    this.toastService.openSuccessToast('Copied to Clipboard');
  }
}
