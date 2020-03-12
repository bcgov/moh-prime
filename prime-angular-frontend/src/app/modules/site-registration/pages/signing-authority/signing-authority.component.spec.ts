import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SigningAuthorityComponent } from './signing-authority.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('SigningAuthorityComponent', () => {
  let component: SigningAuthorityComponent;
  let fixture: ComponentFixture<SigningAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule,
        BrowserAnimationsModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SigningAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
