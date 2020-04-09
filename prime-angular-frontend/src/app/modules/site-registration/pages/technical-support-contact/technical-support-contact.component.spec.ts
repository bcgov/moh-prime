import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { TechnicalSupportContactComponent } from './technical-support-contact.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('TechnicalSupportContactComponent', () => {
  let component: TechnicalSupportContactComponent;
  let fixture: ComponentFixture<TechnicalSupportContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        RouterTestingModule,
        SiteRegistrationModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
