import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeReviewComponent } from './enrollee-review.component';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('EnrolleeReviewComponent', () => {
  let component: EnrolleeReviewComponent;
  let fixture: ComponentFixture<EnrolleeReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SharedModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
