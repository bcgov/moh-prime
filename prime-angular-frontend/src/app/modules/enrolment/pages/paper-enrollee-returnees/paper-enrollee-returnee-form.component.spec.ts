import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaperEnrolleeReturneeFormComponent } from './paper-enrollee-returnee-form.component';

describe('PaperEnrolleeReturneeFormComponent', () => {
  let component: PaperEnrolleeReturneeFormComponent;
  let fixture: ComponentFixture<PaperEnrolleeReturneeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaperEnrolleeReturneeFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaperEnrolleeReturneeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
