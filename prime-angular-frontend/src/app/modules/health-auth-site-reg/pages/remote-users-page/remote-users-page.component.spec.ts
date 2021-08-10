import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { RemoteUsersPageComponent } from './remote-users-page.component';

describe('RemoteUsersPageComponent', () => {
  let component: RemoteUsersPageComponent;
  let fixture: ComponentFixture<RemoteUsersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [
        RemoteUsersPageComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        CapitalizePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
