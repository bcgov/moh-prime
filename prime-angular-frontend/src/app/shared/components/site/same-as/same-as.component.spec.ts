import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { SameAsComponent } from './same-as.component';
import { RouterTestingModule } from '@angular/router/testing';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { APP_CONFIG, APP_DI_CONFIG } from '../../../../app-config.module';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('SameAsComponent', () => {
  let component: SameAsComponent;
  let fixture: ComponentFixture<SameAsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        SiteRegistrationModule
      ],
      declarations: [
        SameAsComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SameAsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
