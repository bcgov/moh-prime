import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteCollectionNoticeComponent } from './site-collection-notice.component';

describe('SiteRegistrationCollectionNoticeComponent', () => {
  let component: SiteCollectionNoticeComponent;
  let fixture: ComponentFixture<SiteCollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteCollectionNoticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteCollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
