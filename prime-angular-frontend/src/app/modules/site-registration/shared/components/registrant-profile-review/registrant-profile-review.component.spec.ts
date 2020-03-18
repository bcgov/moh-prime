import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrantProfileReviewComponent } from './registrant-profile-review.component';
import { SharedModule } from '@shared/shared.module';
import { SiteRegistrationModule } from 'app/modules/site-registration/site-registration.module';

describe('RegistrantProfileReviewComponent', () => {
  let component: RegistrantProfileReviewComponent;
  let fixture: ComponentFixture<RegistrantProfileReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        SharedModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
