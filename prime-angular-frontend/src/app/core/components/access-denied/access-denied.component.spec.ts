import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessDeniedComponent } from './access-denied.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('AccessDeniedComponent', () => {
  let component: AccessDeniedComponent;
  let fixture: ComponentFixture<AccessDeniedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          AccessDeniedComponent
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
    fixture = TestBed.createComponent(AccessDeniedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
