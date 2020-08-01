import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { BusinessLicenceComponent } from './business-licence.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('BusinessLicenceComponent', () => {
  let component: BusinessLicenceComponent;
  let fixture: ComponentFixture<BusinessLicenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BusinessLicenceComponent],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule
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
    fixture = TestBed.createComponent(BusinessLicenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
