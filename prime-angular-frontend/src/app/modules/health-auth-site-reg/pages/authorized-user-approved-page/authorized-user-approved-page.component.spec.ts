import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthorizedUserApprovedPageComponent } from './authorized-user-approved-page.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

describe('AuthorizedUserApprovedPageComponent', () => {
  let component: AuthorizedUserApprovedPageComponent;
  let fixture: ComponentFixture<AuthorizedUserApprovedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule
      ],
      declarations: [
        AuthorizedUserApprovedPageComponent
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
    fixture = TestBed.createComponent(AuthorizedUserApprovedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
