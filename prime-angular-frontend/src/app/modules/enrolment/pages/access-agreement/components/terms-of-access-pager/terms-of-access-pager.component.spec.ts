import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TermsOfAccessPagerComponent } from './terms-of-access-pager.component';

describe('TermsOfAccessPagerComponent', () => {
  let component: TermsOfAccessPagerComponent;
  let fixture: ComponentFixture<TermsOfAccessPagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TermsOfAccessPagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermsOfAccessPagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
