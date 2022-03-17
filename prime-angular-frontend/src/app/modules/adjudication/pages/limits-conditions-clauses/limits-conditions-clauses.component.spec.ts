import faker from 'faker';

import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { LimitsConditionsClausesComponent } from './limits-conditions-clauses.component';
import { of, noop } from 'rxjs';

fdescribe('LimitsConditionsClausesComponent', () => {
  const mockEditor = {
    getContent() {
      return 'some value';
    }
  }

  let component: LimitsConditionsClausesComponent;
  let fixture: ComponentFixture<LimitsConditionsClausesComponent>;

  let spyOnUpdateAccessAgreementNote;

  let mockId;
  let mockNoteValue;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule,
        MatDialogModule
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LimitsConditionsClausesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnUpdateAccessAgreementNote = spyOn((component as any).adjudicationResource, 'updateAccessAgreementNote')
      .and.returnValue(of(noop));
    mockId = faker.random.number();
    mockNoteValue = faker.random.words();
    (component as any).route.snapshot.params.id = mockId;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onSubmit()', () => {
    describe('with valid form', () => {
      it('should call adjudicationResource.updateAccessAgreementNote', () => {
        component.note.setValue(mockNoteValue);
        component.onSubmit();

        expect(spyOnUpdateAccessAgreementNote).toHaveBeenCalledOnceWith(mockId, mockNoteValue);
      });
    });

    describe('with invalid form', () => {
      it('should not call adjudicationResource.updateAccessAgreementNote', () => {
        component.onSubmit();
        expect(spyOnUpdateAccessAgreementNote).not.toHaveBeenCalled();
      });
    });
  });

  describe('testing onUpdate', () => {
    describe('with a null editor', () => {
      it('should return without setting component.preview', () => {
        component.onUpdate({ editor: null });
        expect(component.preview).toBeFalsy();
      });
    });

    describe('with a valid editor', () => {
      it('should set component.preview to a string value', () => {
        component.onUpdate({ editor: mockEditor });
        expect(component.preview).toBe('some value');
      });
    });
  });
});
