import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { AvailableAccessComponent } from './available-access.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AvailableAccessComponent', () => {
  let component: AvailableAccessComponent;
  let fixture: ComponentFixture<AvailableAccessComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [AvailableAccessComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [ReactiveFormsModule,
        RouterTestingModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AvailableAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
