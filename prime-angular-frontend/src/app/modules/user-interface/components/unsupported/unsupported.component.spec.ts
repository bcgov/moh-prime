import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsupportedComponent } from './unsupported.component';
import { SharedModule } from '@shared/shared.module';
import { UserInterfaceModule } from '@ui/user-interface.module';

describe('UnsupportedComponent', () => {
  let component: UnsupportedComponent;
  let fixture: ComponentFixture<UnsupportedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        UserInterfaceModule,
        SharedModule
      ],
      declarations: [
        UnsupportedComponent
      ]
    })
      .compileComponents();
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
