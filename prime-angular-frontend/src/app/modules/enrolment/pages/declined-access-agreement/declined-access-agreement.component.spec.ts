import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedAccessAgreementComponent } from './declined-access-agreement.component';

describe('DeclinedAccessAgreementComponent', () => {
  let component: DeclinedAccessAgreementComponent;
  let fixture: ComponentFixture<DeclinedAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeclinedAccessAgreementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
