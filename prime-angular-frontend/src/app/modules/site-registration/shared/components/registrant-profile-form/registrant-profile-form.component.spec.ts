import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrantProfileFormComponent } from './registrant-profile-form.component';

describe('RegistrantProfileFormComponent', () => {
  let component: RegistrantProfileFormComponent;
  let fixture: ComponentFixture<RegistrantProfileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrantProfileFormComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
