import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbsenceManagementPageComponent } from './absence-management-page.component';

describe('AbsenceManagementPageComponent', () => {
  let component: AbsenceManagementPageComponent;
  let fixture: ComponentFixture<AbsenceManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AbsenceManagementPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AbsenceManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
