import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EscalationNoteComponent } from './escalation-note.component';

describe('EscalationNoteComponent', () => {
  let component: EscalationNoteComponent;
  let fixture: ComponentFixture<EscalationNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EscalationNoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EscalationNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
