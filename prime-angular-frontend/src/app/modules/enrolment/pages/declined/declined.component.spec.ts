import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedComponent } from './declined.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('DeclinedComponent', () => {
  let component: DeclinedComponent;
  let fixture: ComponentFixture<DeclinedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          DeclinedComponent,
          PageHeaderComponent,
          AlertComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
