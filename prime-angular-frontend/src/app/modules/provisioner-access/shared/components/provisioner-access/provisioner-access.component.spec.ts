import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { ProvisionerAccessComponent } from './provisioner-access.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';

describe('ProvisionerAccessComponent', () => {
  let component: ProvisionerAccessComponent;
  let fixture: ComponentFixture<ProvisionerAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ProvisionerAccessComponent,
        PageSubheaderComponent,
        EnrolleePropertyComponent
      ],
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        NgxProgressModule,
        NgxContextualHelpModule,
        DashboardModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvisionerAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
