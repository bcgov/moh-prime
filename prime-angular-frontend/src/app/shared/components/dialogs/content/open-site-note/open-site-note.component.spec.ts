import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenSiteNoteComponent } from './open-site-note.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

describe('OpenSiteNoteComponent', () => {
  let component: OpenSiteNoteComponent;
  let fixture: ComponentFixture<OpenSiteNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        NgxMaterialModule
      ],
      declarations: [OpenSiteNoteComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { }
          }
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            data: {
              siteId: 1,
            }
          }
        },]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenSiteNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
