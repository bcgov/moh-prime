import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSubheaderComponent } from './page-subheader.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('PageSubheaderComponent', () => {
  let component: PageSubheaderComponent;
  let fixture: ComponentFixture<PageSubheaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule
        ],
        declarations: [
          PageSubheaderComponent
        ]
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
