import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { RootRoutesModule } from '@lib/modules/root-routes/root-routes.module';

import { SubmissionConfirmationPageComponent } from './submission-confirmation-page.component';

describe('SubmissionConfirmationPageComponent', () => {
  let component: SubmissionConfirmationPageComponent;
  let fixture: ComponentFixture<SubmissionConfirmationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RootRoutesModule,
        RouterTestingModule
      ],
      declarations: [SubmissionConfirmationPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionConfirmationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
