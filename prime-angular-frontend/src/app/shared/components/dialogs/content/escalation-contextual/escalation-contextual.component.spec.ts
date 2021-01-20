import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EscalationContextualComponent } from './escalation-contextual.component';

describe('EscalationContextualComponent', () => {
  let component: EscalationContextualComponent;
  let fixture: ComponentFixture<EscalationContextualComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EscalationContextualComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EscalationContextualComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
