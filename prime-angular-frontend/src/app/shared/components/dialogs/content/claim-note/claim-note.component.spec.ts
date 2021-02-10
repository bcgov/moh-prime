import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimNoteComponent } from './claim-note.component';

describe('ClaimNoteComponent', () => {
  let component: ClaimNoteComponent;
  let fixture: ComponentFixture<ClaimNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClaimNoteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
