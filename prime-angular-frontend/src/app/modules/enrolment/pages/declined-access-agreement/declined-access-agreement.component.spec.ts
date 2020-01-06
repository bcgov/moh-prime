import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedAccessAgreementComponent } from './declined-access-agreement.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('DeclinedAccessAgreementComponent', () => {
  let component: DeclinedAccessAgreementComponent;
  let fixture: ComponentFixture<DeclinedAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          DeclinedAccessAgreementComponent,
          PageHeaderComponent,
          AlertComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
