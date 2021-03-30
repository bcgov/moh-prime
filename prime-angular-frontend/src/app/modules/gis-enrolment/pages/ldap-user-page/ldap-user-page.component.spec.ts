import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { LdapUserPageComponent } from './ldap-user-page.component';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';

describe('LdapUserPageComponent', () => {
  let component: LdapUserPageComponent;
  let fixture: ComponentFixture<LdapUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        BrowserAnimationsModule,
        NgxMaterialModule
      ],
      declarations: [
        LdapUserPageComponent,
        YesNoPipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LdapUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
