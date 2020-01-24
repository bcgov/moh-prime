import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RuAccessTermsComponent } from './ru-access-terms.component';

describe('RuAccessTermsComponent', () => {
  let component: RuAccessTermsComponent;
  let fixture: ComponentFixture<RuAccessTermsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RuAccessTermsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RuAccessTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
