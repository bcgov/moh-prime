import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicalSupportContactComponent } from './technical-support-contact.component';

describe('TechnicalSupportContactComponent', () => {
  let component: TechnicalSupportContactComponent;
  let fixture: ComponentFixture<TechnicalSupportContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TechnicalSupportContactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
