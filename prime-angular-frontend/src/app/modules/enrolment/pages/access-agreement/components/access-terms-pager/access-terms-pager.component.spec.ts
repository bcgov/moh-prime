import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermsPagerComponent } from './access-terms-pager.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('AccessTermPagerComponent', () => {
  let component: AccessTermsPagerComponent;
  let fixture: ComponentFixture<AccessTermsPagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          EnrolmentModule
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsPagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
