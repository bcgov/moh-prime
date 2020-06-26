import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationActionsComponent } from './site-registration-actions.component';

describe('SiteRegistrationActionsComponent', () => {
  let component: SiteRegistrationActionsComponent;
  let fixture: ComponentFixture<SiteRegistrationActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
