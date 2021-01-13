import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeSelfDeclarationsComponent } from './enrollee-self-declarations.component';

describe('EnrolleeSelfDeclarationsComponent', () => {
  let component: EnrolleeSelfDeclarationsComponent;
  let fixture: ComponentFixture<EnrolleeSelfDeclarationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeSelfDeclarationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeSelfDeclarationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
