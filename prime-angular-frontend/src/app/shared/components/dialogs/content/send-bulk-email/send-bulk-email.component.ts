import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-send-bulk-email',
  templateUrl: './send-bulk-email.component.html',
  styleUrls: ['./send-bulk-email.component.scss']
})
export class SendBulkEmailComponent implements OnInit {
  public title: string;
  
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) { 
    this.title = data.title;
  }
  
  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSelect(email: string): void {
    this.dialogRef.close(email);
  }

  public ngOnInit(): void {
  }

}
