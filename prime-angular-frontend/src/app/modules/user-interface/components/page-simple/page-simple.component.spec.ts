import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSimpleComponent } from './page-simple.component';
import { UserInterfaceModule } from '@ui/user-interface.module';

describe('PageSimpleComponent', () => {
  let component: PageSimpleComponent;
  let fixture: ComponentFixture<PageSimpleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          UserInterfaceModule
        ],
        declarations: []
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSimpleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
