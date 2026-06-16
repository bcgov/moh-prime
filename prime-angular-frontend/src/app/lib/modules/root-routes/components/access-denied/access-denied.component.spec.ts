import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { RootRoutesModule } from '../../root-routes.module';
import { AccessDeniedComponent } from './access-denied.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AccessDeniedComponent', () => {
  let component: AccessDeniedComponent;
  let fixture: ComponentFixture<AccessDeniedComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        RootRoutesModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessDeniedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
