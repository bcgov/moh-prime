import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClipboardIconComponent } from './clipboard-icon.component';

describe('ClipboardIconComponent', () => {
  let component: ClipboardIconComponent;
  let fixture: ComponentFixture<ClipboardIconComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClipboardIconComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClipboardIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
