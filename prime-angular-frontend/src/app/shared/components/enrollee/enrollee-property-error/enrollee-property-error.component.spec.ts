import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolleePropertyErrorComponent } from './enrollee-property-error.component';
import { MatTooltipModule } from '@angular/material/tooltip';

describe('EnrolleePropertyErrorComponent', () => {
  let component: EnrolleePropertyErrorComponent;
  let fixture: ComponentFixture<EnrolleePropertyErrorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule,
        BrowserAnimationsModule,
        MatTooltipModule
      ],
      declarations: [],
      providers: [],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePropertyErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
