import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { OrganizationInformationPageComponent } from './organization-information-page.component';

describe('OrganizationInformationPageComponent', () => {
  let component: OrganizationInformationPageComponent;
  let fixture: ComponentFixture<OrganizationInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
      ],
      declarations: [OrganizationInformationPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
