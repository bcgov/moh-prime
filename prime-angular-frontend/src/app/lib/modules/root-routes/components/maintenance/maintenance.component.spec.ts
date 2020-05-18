import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RootRoutesModule } from '../../root-routes.module';
import { MaintenanceComponent } from './maintenance.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('MaintenanceComponent', () => {
  let component: MaintenanceComponent;
  let fixture: ComponentFixture<MaintenanceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RootRoutesModule
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
