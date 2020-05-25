import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteReviewComponent } from './site-review.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('SiteReviewComponent', () => {
  let component: SiteReviewComponent;
  let fixture: ComponentFixture<SiteReviewComponent>;

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
