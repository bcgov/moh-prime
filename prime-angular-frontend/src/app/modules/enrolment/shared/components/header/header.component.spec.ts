import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { HeaderComponent } from './header.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          HeaderComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
