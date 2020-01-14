import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeContactComponent } from './prime-contact.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('PrimeContactComponent', () => {
  let component: PrimeContactComponent;
  let fixture: ComponentFixture<PrimeContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [PrimeContactComponent],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
