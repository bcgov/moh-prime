import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewStatusContentComponent } from './review-status-content.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('ReviewStatusContentComponent', () => {
  let component: ReviewStatusContentComponent;
  let fixture: ComponentFixture<ReviewStatusContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule,
      ],
      declarations: []
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewStatusContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
