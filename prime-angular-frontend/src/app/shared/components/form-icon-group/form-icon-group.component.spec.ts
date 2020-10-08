import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { FormIconGroupComponent } from './form-icon-group.component';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';

describe('FormIconGroupComponent', () => {
  let component: FormIconGroupComponent;
  let fixture: ComponentFixture<FormIconGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule,
          NgxMaterialModule
        ],
        declarations: [
          FormIconGroupComponent
        ],
        providers: [
          {
            provide: AuthService,
            useClass: MockAuthService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormIconGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
