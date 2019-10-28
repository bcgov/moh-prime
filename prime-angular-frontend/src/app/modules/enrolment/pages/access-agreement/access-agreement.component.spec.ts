import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessAgreementComponent } from './access-agreement.component';

describe('AccessAgreementComponent', () => {
  let component: AccessAgreementComponent;
  let fixture: ComponentFixture<AccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessAgreementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
