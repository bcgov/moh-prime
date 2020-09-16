import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteUserReviewComponent } from './remote-user-review.component';

describe('RemoteUserReviewComponent', () => {
  let component: RemoteUserReviewComponent;
  let fixture: ComponentFixture<RemoteUserReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoteUserReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUserReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
