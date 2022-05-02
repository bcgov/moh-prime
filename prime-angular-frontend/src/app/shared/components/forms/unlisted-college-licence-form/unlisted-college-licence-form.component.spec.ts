import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnlistedCollegeLicenceFormComponent } from './unlisted-college-licence-form.component';

describe('UnlistedCollegeLicenceFormComponent', () => {
  let component: UnlistedCollegeLicenceFormComponent;
  let fixture: ComponentFixture<UnlistedCollegeLicenceFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnlistedCollegeLicenceFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UnlistedCollegeLicenceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
