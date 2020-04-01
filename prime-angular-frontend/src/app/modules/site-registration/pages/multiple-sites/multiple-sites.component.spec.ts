import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MultipleSitesComponent } from './multiple-sites.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('MultipleSitesComponent', () => {
  let component: MultipleSitesComponent;
  let fixture: ComponentFixture<MultipleSitesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        SiteRegistrationModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MultipleSitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
