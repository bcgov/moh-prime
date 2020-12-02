import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OboSiteFormComponent } from './obo-site-form.component';

describe('OboSiteFormComponent', () => {
  let component: OboSiteFormComponent;
  let fixture: ComponentFixture<OboSiteFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OboSiteFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OboSiteFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
