import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { PaperEnrolmentModule } from '@paper-enrolment/paper-enrolment.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { OboSitesPageComponent } from './obo-sites-page.component';

describe('OboSitesPageComponent', () => {
  let component: OboSitesPageComponent;
  let fixture: ComponentFixture<OboSitesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OboSitesPageComponent],
      imports: [
        NgxMaterialModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OboSitesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
