import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeSupportEmailComponent } from './prime-support-email.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('PrimeSupportEmailComponent', () => {
  let component: PrimeSupportEmailComponent;
  let fixture: ComponentFixture<PrimeSupportEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PrimeSupportEmailComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeSupportEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
