import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { AccessAgreementComponent } from './access-agreement.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SubHeaderComponent } from '@shared/components/sub-header/sub-header.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('AccessAgreementComponent', () => {
  let component: AccessAgreementComponent;
  let fixture: ComponentFixture<AccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule
        ],
        declarations: [
          AccessAgreementComponent,
          SubHeaderComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
