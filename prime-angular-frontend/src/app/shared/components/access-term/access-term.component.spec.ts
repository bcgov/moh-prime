import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermComponent } from './access-term.component';
import { SafePipe } from '@shared/pipes/safe.pipe';

describe('AccessTermComponent', () => {
  let component: AccessTermComponent;
  let fixture: ComponentFixture<AccessTermComponent>;
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [
        AccessTermComponent,
        SafePipe
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
