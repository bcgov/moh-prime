import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeEmailComponent } from './prime-email.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('PrimeEmailComponent', () => {
  let component: PrimeEmailComponent;
  let fixture: ComponentFixture<PrimeEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          PrimeEmailComponent
        ],
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
    fixture = TestBed.createComponent(PrimeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
