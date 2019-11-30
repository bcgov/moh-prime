import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MoaAccessAgreementComponent } from './moa-access-agreement.component';

describe('MoaAccessAgreementComponent', () => {
  let component: MoaAccessAgreementComponent;
  let fixture: ComponentFixture<MoaAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoaAccessAgreementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoaAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
