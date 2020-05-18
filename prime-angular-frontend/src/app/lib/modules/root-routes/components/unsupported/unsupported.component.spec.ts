import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RootRoutesModule } from '../../root-routes.module';
import { UnsupportedComponent } from './unsupported.component';

describe('UnsupportedComponent', () => {
  let component: UnsupportedComponent;
  let fixture: ComponentFixture<UnsupportedComponent>;

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
    fixture = TestBed.createComponent(UnsupportedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
