import { async, ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { ContactInformationFormComponent } from './contact-information-form.component';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

describe('ContactInformationComponent', () => {
  let component: ContactInformationFormComponent;
  let fixture: ComponentFixture<ContactInformationFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ],
      declarations: [
        ContactInformationFormComponent
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactInformationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(inject(
    [EnrolmentFormStateService],
    (enrolmentFormStateService: EnrolmentFormStateService) => {
      fixture = TestBed.createComponent(ContactInformationFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentFormStateService.bcscDemographicForm;
      fixture.detectChanges();
    }
  ));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
