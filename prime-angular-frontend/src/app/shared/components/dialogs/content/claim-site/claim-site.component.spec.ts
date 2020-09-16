import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimSiteComponent } from './claim-site.component';
import { SharedModule } from '@shared/shared.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { APP_DI_CONFIG, APP_CONFIG } from 'app/app-config.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('ClaimSiteComponent', () => {
  let component: ClaimSiteComponent;
  let fixture: ComponentFixture<ClaimSiteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        NgxBusyModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        SharedModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { }
          }
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {}
        },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
