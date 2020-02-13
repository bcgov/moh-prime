import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeSupportEmailComponent } from './prime-support-email.component';

describe('PrimeSupportEmailComponent', () => {
  let component: PrimeSupportEmailComponent;
  let fixture: ComponentFixture<PrimeSupportEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimeSupportEmailComponent ]
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
