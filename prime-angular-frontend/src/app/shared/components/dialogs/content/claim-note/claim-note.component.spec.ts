import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/compiler';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';

import { ClaimNoteComponent } from './claim-note.component';

describe('ClaimNoteComponent', () => {
  let component: ClaimNoteComponent;
  let fixture: ComponentFixture<ClaimNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MatDialogModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      declarations: [
        ClaimNoteComponent,
        CapitalizePipe
      ],
      providers: [
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            data: {
              reassign: 1,
              type: 1
            }
          }
        },
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
        KeycloakService
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
