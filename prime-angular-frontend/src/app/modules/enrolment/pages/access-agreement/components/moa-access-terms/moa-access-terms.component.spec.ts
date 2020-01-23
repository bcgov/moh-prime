import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MoaAccessTermsComponent } from './moa-access-terms.component';

describe('MoaAccessTermsComponent', () => {
  let component: MoaAccessTermsComponent;
  let fixture: ComponentFixture<MoaAccessTermsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoaAccessTermsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoaAccessTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
