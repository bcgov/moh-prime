import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationCollectionNoticeComponent } from './site-registration-collection-notice.component';

describe('SiteRegistrationCollectionNoticeComponent', () => {
  let component: SiteRegistrationCollectionNoticeComponent;
  let fixture: ComponentFixture<SiteRegistrationCollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationCollectionNoticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationCollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
