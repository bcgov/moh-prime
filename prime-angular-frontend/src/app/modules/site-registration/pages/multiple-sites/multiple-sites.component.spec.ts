import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleSitesComponent } from './multiple-sites.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('MultipleSitesComponent', () => {
  let component: MultipleSitesComponent;
  let fixture: ComponentFixture<MultipleSitesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule
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
