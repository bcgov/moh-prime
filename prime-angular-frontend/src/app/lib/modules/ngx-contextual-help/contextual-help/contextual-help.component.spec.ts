import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContextualHelpComponent } from './contextual-help.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('ContextualHelpComponent', () => {
  let component: ContextualHelpComponent;
  let fixture: ComponentFixture<ContextualHelpComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          ContextualHelpComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualHelpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
