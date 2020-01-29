import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvisionerAccessComponent } from './provisioner-access.component';
import { HeaderComponent } from '@shared/components/header/header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleePrivilegesComponent } from '@shared/components/enrollee/enrollee-privileges/enrollee-privileges.component';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';

describe('ProvisionerAccessComponent', () => {
  let component: ProvisionerAccessComponent;
  let fixture: ComponentFixture<ProvisionerAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        NgxProgressModule,
        NgxContextualHelpModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      declarations: [
        ProvisionerAccessComponent,
        HeaderComponent,
        PageSubheaderComponent,
        EnrolleePrivilegesComponent,
        EnrolleePropertyComponent
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
