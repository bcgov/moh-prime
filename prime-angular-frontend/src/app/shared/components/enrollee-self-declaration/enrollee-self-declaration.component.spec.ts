import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeSelfDeclarationComponent } from './enrollee-self-declaration.component';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';

describe('EnrolleeSelfDeclarationComponent', () => {
  let component: EnrolleeSelfDeclarationComponent;
  let fixture: ComponentFixture<EnrolleeSelfDeclarationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeSelfDeclarationComponent,
          YesNoPipe
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeSelfDeclarationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
