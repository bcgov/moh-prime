import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { RootRoutesModule } from '../../root-routes.module';
import { HelpComponent } from './help.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HelpComponent', () => {
  let component: HelpComponent;
  let fixture: ComponentFixture<HelpComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RootRoutesModule],
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
    fixture = TestBed.createComponent(HelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
