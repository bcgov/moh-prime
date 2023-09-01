import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { RegulatoryOverviewComponent } from './regulatory-overview.component';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('RegulatoryOverviewComponent', () => {
  let component: RegulatoryOverviewComponent;
  let fixture: ComponentFixture<RegulatoryOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      declarations: [
        RegulatoryOverviewComponent,
        DefaultPipe,
        ConfigCodePipe,
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
