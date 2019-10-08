import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmDiscardChangesDialogComponent } from './confirm-discard-changes-dialog.component';

describe('ConfirmDiscardChangesDialogComponent', () => {
  let component: ConfirmDiscardChangesDialogComponent;
  let fixture: ComponentFixture<ConfirmDiscardChangesDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmDiscardChangesDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmDiscardChangesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
