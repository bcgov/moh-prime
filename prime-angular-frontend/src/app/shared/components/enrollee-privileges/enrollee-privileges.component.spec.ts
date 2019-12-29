import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleePrivilegesComponent } from './enrollee-privileges.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';


describe('EnrolleePrivilegesComponent', () => {
  let component: EnrolleePrivilegesComponent;
  let fixture: ComponentFixture<EnrolleePrivilegesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        EnrolleePrivilegesComponent,
        PageSubheaderComponent,
        DefaultPipe
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePrivilegesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
