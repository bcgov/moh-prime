import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlertComponent } from './alert.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('AlertComponent', () => {
  let component: AlertComponent;
  let fixture: ComponentFixture<AlertComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          AlertComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
