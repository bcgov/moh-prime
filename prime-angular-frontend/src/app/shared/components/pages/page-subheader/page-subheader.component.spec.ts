import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSubheaderComponent } from './page-subheader.component';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('PageSubheaderComponent', () => {
  let component: PageSubheaderComponent;
  let fixture: ComponentFixture<PageSubheaderComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule
        ],
        declarations: [
          PageSubheaderComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSubheaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
