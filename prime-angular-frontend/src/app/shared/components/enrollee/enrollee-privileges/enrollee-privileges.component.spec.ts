import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { EnrolleePrivilegesComponent } from './enrollee-privileges.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';


describe('EnrolleePrivilegesComponent', () => {
  let component: EnrolleePrivilegesComponent;
  let fixture: ComponentFixture<EnrolleePrivilegesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule
      ],
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
