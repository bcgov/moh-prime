import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAgreementNotesComponent } from './user-agreement-notes.component';

describe('UserAgreementNotesComponent', () => {
  let component: UserAgreementNotesComponent;
  let fixture: ComponentFixture<UserAgreementNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserAgreementNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserAgreementNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
