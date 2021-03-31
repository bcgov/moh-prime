import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendBulkEmailComponent } from './send-bulk-email.component';
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {NgxMaterialModule} from '@lib/modules/ngx-material/ngx-material.module';
import {SendEmailComponent} from '@shared/components/dialogs/content/send-email/send-email.component';
import {APP_CONFIG, APP_DI_CONFIG} from '../../../../../app-config.module';

describe('SendBulkEmailComponent', () => {
  let component: SendBulkEmailComponent;
  let fixture: ComponentFixture<SendBulkEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MatDialogModule,
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      declarations: [SendEmailComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MatDialogRef,
          useValue: {}
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            title: 'Send Bulk Email'
          }
        },
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SendBulkEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
