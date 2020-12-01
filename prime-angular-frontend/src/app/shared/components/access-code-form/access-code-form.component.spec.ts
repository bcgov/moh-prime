import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCodeFormComponent } from './access-code-form.component';

describe('AccessCodeFormComponent', () => {
  let component: AccessCodeFormComponent;
  let fixture: ComponentFixture<AccessCodeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessCodeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCodeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
