import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteReviewComponent } from './site-review.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('SiteReviewComponent', () => {
  let component: SiteReviewComponent;
  let fixture: ComponentFixture<SiteReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
