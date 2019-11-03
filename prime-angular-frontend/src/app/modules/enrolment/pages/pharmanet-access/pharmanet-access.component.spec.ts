import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { PharmanetAccessComponent } from './pharmanet-access.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { SubHeaderComponent } from '@shared/components/sub-header/sub-header.component';

describe('PharmanetAccessComponent', () => {
  let component: PharmanetAccessComponent;
  let fixture: ComponentFixture<PharmanetAccessComponent>;

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
          PharmanetAccessComponent,
          SubHeaderComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useValue: MockConfigService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmanetAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create PharmanetAccessComponent', () => {
    expect(component).toBeTruthy();
  });
});
