import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationContainerComponent } from './site-registration-container.component';

describe('SiteRegistrationContainerComponent', () => {
  let component: SiteRegistrationContainerComponent;
  let fixture: ComponentFixture<SiteRegistrationContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
