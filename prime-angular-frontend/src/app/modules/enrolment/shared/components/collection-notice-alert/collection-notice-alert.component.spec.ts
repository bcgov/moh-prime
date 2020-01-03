import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionNoticeAlertComponent } from './collection-notice-alert.component';

describe('CollectionNoticeAlertComponent', () => {
  let component: CollectionNoticeAlertComponent;
  let fixture: ComponentFixture<CollectionNoticeAlertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CollectionNoticeAlertComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticeAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
