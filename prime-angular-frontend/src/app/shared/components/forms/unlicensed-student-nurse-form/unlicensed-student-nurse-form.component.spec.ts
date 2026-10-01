import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnlicensedStudentNurseFormComponent } from './unlicensed-student-nurse-form.component';

describe('UnlicensedStudentNurseFormComponent', () => {
  let component: UnlicensedStudentNurseFormComponent;
  let fixture: ComponentFixture<UnlicensedStudentNurseFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UnlicensedStudentNurseFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnlicensedStudentNurseFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
