import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { RootRoutesModule } from '../../root-routes.module';
import { PageNotFoundComponent } from './page-not-found.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('PageNotFoundComponent', () => {
  let component: PageNotFoundComponent;
  let fixture: ComponentFixture<PageNotFoundComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RootRoutesModule,
        RouterTestingModule],
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
    fixture = TestBed.createComponent(PageNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
