import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RootRoutesModule } from '../../root-routes.module';
import { PageSimpleComponent } from './page-simple.component';

describe('PageSimpleComponent', () => {
  let component: PageSimpleComponent;
  let fixture: ComponentFixture<PageSimpleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RootRoutesModule
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
