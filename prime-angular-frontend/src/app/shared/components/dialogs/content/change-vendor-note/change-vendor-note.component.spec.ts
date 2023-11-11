import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ChangeVendorNoteComponent } from './change-vendor-note.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('ChangeVendorNoteComponent', () => {
  let component: ChangeVendorNoteComponent;
  let fixture: ComponentFixture<ChangeVendorNoteComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { },
            updateSize: () => { }
          }
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            data: {
              siteId: 1,
              title: "testing",
              vendorChangeText: "from a to b",
              vendorCode: 1,
            }
          }
        },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChangeVendorNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
