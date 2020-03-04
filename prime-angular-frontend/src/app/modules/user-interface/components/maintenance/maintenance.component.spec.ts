import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceComponent } from './maintenance.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { UserInterfaceModule } from '@ui/user-interface.module';

describe('MaintenanceComponent', () => {
  let component: MaintenanceComponent;
  let fixture: ComponentFixture<MaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          UserInterfaceModule
        ],
        declarations: [],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
