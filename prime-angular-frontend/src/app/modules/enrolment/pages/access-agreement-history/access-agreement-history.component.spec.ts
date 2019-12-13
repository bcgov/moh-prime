import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessAgreementHistoryComponent } from './access-agreement-history.component';

describe('AccessAgreementHistoryComponent', () => {
  let component: AccessAgreementHistoryComponent;
  let fixture: ComponentFixture<AccessAgreementHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessAgreementHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
