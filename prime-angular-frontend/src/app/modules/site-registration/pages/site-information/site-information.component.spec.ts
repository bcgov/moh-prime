import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteInformationComponent } from './site-information.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('SiteInformationComponent', () => {
  let component: SiteInformationComponent;
  let fixture: ComponentFixture<SiteInformationComponent>;

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
    fixture = TestBed.createComponent(SiteInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
