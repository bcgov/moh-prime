import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SharedModule } from '@shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { OrganizationReviewComponent } from './organization-review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('OrganizationReviewComponent', () => {
  let component: OrganizationReviewComponent;
  let fixture: ComponentFixture<OrganizationReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OrganizationReviewComponent],
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
    fixture = TestBed.createComponent(OrganizationReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
