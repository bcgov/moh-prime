import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermComponent } from './access-term.component';
import { SafeHtmlPipe } from '@shared/pipes/safe-html.pipe';

describe('AccessTermComponent', () => {
  let component: AccessTermComponent;
  let fixture: ComponentFixture<AccessTermComponent>;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AccessTermComponent, SafeHtmlPipe],
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
