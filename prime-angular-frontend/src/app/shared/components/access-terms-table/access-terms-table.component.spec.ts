import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermsTableComponent } from './access-terms-table.component';

describe('AccessTermsTableComponent', () => {
  let component: AccessTermsTableComponent;
  let fixture: ComponentFixture<AccessTermsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessTermsTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
