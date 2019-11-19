import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RuAccessAgreementComponent } from './ru-access-agreement.component';

describe('RuAccessAgreementComponent', () => {
  let component: RuAccessAgreementComponent;
  let fixture: ComponentFixture<RuAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RuAccessAgreementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RuAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
