import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermsTableComponent } from './access-terms-table.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '@shared/shared.module';

describe('AccessTermsTableComponent', () => {
  let component: AccessTermsTableComponent;
  let fixture: ComponentFixture<AccessTermsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule,
        SharedModule
      ],
      declarations: []
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
